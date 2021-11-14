using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
/*if (lastVisited.GetComponent<Peron>() != null && next == lastVisited.GetComponent<Peron>().nextPoint)
                {
                    Debug.Log("Semafor kurde");
                    lastVisited.GetComponent<Peron>().Wait = true;
                    lastVisited.GetComponent<Peron>().Light.Toggle();
                }
                */


/*
     zwrotnice
        0 - tor statyczny
        1 - tor aktywny
        2 - tor nieaktywny

    pkt za zwrotnicą
        0 - tor statyczny
        1 - zwrotnica

    */

//Cofanie pociągów i losowe eventy są wykomentowane, ale są w kodzie

public partial class Train : MonoBehaviour, IPointerDownHandler
{
    Generator gen;
    [HideInInspector] public float StopTime;
    private float basicVelocity = 0;
    public float velocity = 0;//prędkość chwilowa pociągu
    private float acceleration = 0.3f; //wartość przyspieszenia
    private float decceleration = 0.7f; //wartość zwalniania
    private float maxV = 1.8f; //prędkość maksymalna
    private float minV = 0.5f;//prędkość minimalna
    [HideInInspector] public float slowDist; //odległość do zwolnienia przed zakrętem
    [HideInInspector] public float stopDist; //odległość do pkt zatrzymania
    [HideInInspector] public float brake; //odległość hamowania
    [HideInInspector] public float distance; //odleglosc do najbliższego pkt
    public TrainRecord TrainRecord;
    public float realVelocity; //prędkość przemieszczania między punktami
    public Rekord rekord;
    public Waypoint startingPoint;
    public Waypoint endingPoint;
    public Waypoint next;
    public Waypoint lastVisited;
    private float Heading;              //pewnie będzie niepotrzebne
    public Waypoint toStop;
    private Waypoint tempToStop;
    private Vector3 trainPosition;
    private Waypoint krytyczny;
    [HideInInspector] public float turnDist;
    [HideInInspector] public bool PlatformStand = false;
    [HideInInspector] public float timer = 0;
    private Waypoint temp1, temp2;
    public bool Go = false;
    public  bool moveToStart = false;
    public float PlatTimer;
    public float HaltDelay;
    public int ArrivalHour = 100000;
    public int DepartureHour = 100000;
    public float EventTimer;
    [HideInInspector] public bool Arrived = false;
    [HideInInspector] public bool Departed = false;
    public int UsedPlatform;
    private bool AssignedUsedPlatform = false;
    private bool ArrivalPoints = false;
    private bool JunctionStop = false;
    public string Status = "-";
    string IDD;
    public float DelayWaitingTimer;
    float distanceFromLV; //odległość od laxt visited
    Vector3 lvNext;
    public Car wagon;
    Vector3 trainAngle;
    Waypoint tempReverse;
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    public bool TurningBack = false;
    float speedCorrection;
    public List<Waypoint> Path = new List<Waypoint>();


    public List<Vector3> LocomotivePositions = new List<Vector3>();

    public void AddLocomotivePosition()
    {
        if (LocomotivePositions.Count == 10) LocomotivePositions.RemoveAt(9);

        LocomotivePositions.Add(transform.position);
        //Debug.Log(LocomotivePositions);
    }
    public void TrainEntryApproval()
    {
        Go = true;
    }
    public void AssignNextPoint()
    {
        var origin = lastVisited;
        lastVisited = next;
        next = next.Ways.ToList().FirstOrDefault(x => x != origin);
        //if (next.ways[0] == lastVisited)
        //{
        //    if (next.ways[1] != null)
        //    {
        //        lastVisited = next;
        //        next = next.ways[1];
        //    }
        //}
        //else if (next.ways[1] == lastVisited)
        //{
        //    if (next.ways[0] != null)
        //    {
        //        lastVisited = next;
        //        next = next.ways[0];
        //    }
        //}



        //else if (next.ways[1] != null)
        //{
        //    if (next.ways[0] == lastVisited)
        //    {
        //        lastVisited = next;
        //        next = next.ways[1];
        //    }
        //    else if (next.ways[1] == lastVisited)
        //    {
        //        lastVisited = next;
        //        next = next.ways[0];
        //    }
        //}

        Path.Add(next);
    }
    void JunctionCollisionCheck()
    {
        if (next.Ways.Count > 2|| lastVisited.Ways.Count > 2)
        {
            if (next.Ways.Count > 2)
            {

                temp1 = next;
            }
            else if (lastVisited.Ways.Count > 2)
            {
                temp1 = lastVisited;
            }
            else temp1 = null;

            if (temp1.train != null && temp1.train != this)
            {
                gen.GameOver(1);
                //Debug.Log("GameOver - two trains on the junction");
            }
            else temp1.train = this;
        }
        if (temp1 != null && lastVisited != temp1 && next != temp1)
        {
            temp1.train = null;
        }
    }
    public Waypoint semafor;
    void SemaforCollisionCheck()
    {

        Waypoint next2 = next;
        Waypoint LV = lastVisited;
        if (startingPoint != null)
        {
            if (semafor != null && lastVisited == semafor) semafor.train = null;
            while (next2.Ways[0] != LV || next2.Ways.Count > 1)
            {
                if (LV == next2.Ways[0])
                {
                    LV = next2;
                    next2 = next2.Ways[1];
                }
                else if (LV == next2.Ways[1])
                {
                    LV = next2;
                    next2 = next2.Ways[0];
                }

                //zatrzymanie przed semaforem
                if (LV.GetComponent<Semaphore>() != null || LV.GetComponent<PlatformTrack>() != null)
                {

                    semafor = LV;
                    break;
                }
                else semafor = null;
            }

            if (semafor != null)
            {
                if (semafor.train == null) semafor.train = this;
                else if (semafor.train != this)
                {
                    gen.GameOver(0);
                    //Debug.Log("GameOver - two trains going to the same semafor");
                }
            }

        }
    }
    void TrainAudio()
    {
        if (moveToStart)
        {
            transform.Find("Buzz").GetComponent<AudioSource>().mute = false;
            transform.Find("Pitch").GetComponent<AudioSource>().mute = false;
            transform.Find("Buzz").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/tlo");
            // this.gameObject.GetComponent<AudioSource>().pitch = (0.23f*velocity/gen.CompressionRate + 0.7f);
            if (velocity / gen.CompressionRate < 0.6f)
            {
                if(velocity/gen.CompressionRate==0) transform.Find("Pitch").GetComponent<AudioSource>().mute = true;
                else transform.Find("Pitch").GetComponent<AudioSource>().mute = false;
                transform.Find("Buzz").GetComponent<AudioSource>().pitch = 1f;
                transform.Find("Pitch").GetComponent<AudioSource>().pitch = 1f;
            }
            else if (velocity / gen.CompressionRate >= 0.6f && velocity / gen.CompressionRate < 1.4f)
            {
                transform.Find("Buzz").GetComponent<AudioSource>().pitch = 1.1f;
                transform.Find("Pitch").GetComponent<AudioSource>().pitch = (0.63f * velocity / gen.CompressionRate + 0.45f);
            }
            else if (velocity / gen.CompressionRate >= 1.4f)
            {
                transform.Find("Pitch").GetComponent<AudioSource>().pitch = (0.23f * velocity / gen.CompressionRate + 0.4f);
                transform.Find("Buzz").GetComponent<AudioSource>().pitch = 0.8f;
            }
        }
        
    }
    void MoveToStart()
    {
        lastVisited = startingPoint;
        this.transform.position = startingPoint.transform.position;
        next = startingPoint.Ways[0];
        Path.Add(next);
    }
    void DistancesCalculation()
    {
        //obliczanie dystansu do pkt zatrzymania
        if (toStop == null) stopDist = 999999999999999999;
        else if ((next.GetComponent<Semaphore>() != null || next.GetComponent<PlatformTrack>() != null)) stopDist = Vector2.Distance(this.transform.position, toStop.transform.position) - 0.45f; //żeby trafiać bardziej w  semafor
        else stopDist = Vector2.Distance(this.transform.position, toStop.transform.position) - 0.7f;

        //oblicznie dystnasu do kolejnego pkt
        distance = Vector2.Distance(this.transform.position, next.transform.position);

        //oblicznie dystansu do zwalniania przed zakrętem
        if (velocity > minV) slowDist = ((velocity * ((velocity - minV) / decceleration)) - (((velocity - minV) * (velocity - minV)) / (2 * decceleration)) + 0.7f);
        else slowDist = 0;

        //obliczanie długości hamowania
        brake = ((velocity * velocity) / (2 * decceleration));

        if (krytyczny != null) turnDist = Vector2.Distance(this.transform.position, krytyczny.transform.position);
        else turnDist = 999999999999999999f; // trzeba tutaj to na warunek zmienic (zeby nie bylo takiego badziewia)
    }
    void VelocityChange()
    {
        if (stopDist > brake)
        {
            if (turnDist > slowDist && velocity < maxV) velocity = velocity + acceleration * Time.deltaTime; //przyspieszanie pociągu na prostej
            else if (turnDist < slowDist)
            {
                if (velocity > minV) velocity = velocity - decceleration * Time.deltaTime; //hamowanie pociagu przed zakrętem
                else if (velocity < minV) velocity = velocity + acceleration * Time.deltaTime; ; //ruszanie pociągu po zatrzymaniu
            }
        }
        else if (stopDist < brake)
        {
            if (velocity > 0) velocity = velocity - decceleration * Time.deltaTime;
            else if (velocity < 0) velocity = 0;
        }

       
    }
    void StatusSet()
    {
        if (TrainRecord.ArrivalTime - TrainGame.Generator().clock < 180 && !Go) Status = "Waiting approval";
        else if (Go && velocity > 0) Status = "Moving";
        else if (Go && moveToStart && distance < 0.5f && next.GetComponent<PlatformTrack>() != null && next.GetComponent<PlatformTrack>().NextPoint != lastVisited && velocity == 0) Status = "At platform " + UsedPlatform.ToString();
        else if (Go && velocity == 0) Status = "Waiting";

    }
    public void OnPointerDown (PointerEventData data)//podwójne kliniecie
    {
       
        clicked++;
        if (clicked == 1) clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
           // Debug.Log("Double click: " + IDD);
            if (!TurningBack) TurningBack = true;
            else TurningBack = false;
            clicked = 0;
            clicktime = 0;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    } 
    void GoReverse()
    {
        if (velocity > 0) velocity = velocity - decceleration * Time.deltaTime;
        else if (velocity <= 0)
        {
            tempReverse = next;
            next = lastVisited;
            lastVisited = tempReverse;
            TurningBack = false;
        }

    } 
    void ColorTrain()
    {
        if (rekord.TrainColorInfo == 0)
        {
            GetComponent<Renderer>().material.color = new Color32(255, 164, 4, 220);
        }
        else if (rekord.TrainColorInfo ==1)
        {
            GetComponent<Renderer>().material.color = new Color32(62, 219, 28, 220);
        }
        else if (rekord.TrainColorInfo ==2)
        {
            //to jest przezroczysty pociąg, TO ZMIENIĆ
            GetComponent<Renderer>().material.color = new Color32(62, 219, 28, 0);
        }
        else if (rekord.TrainColorInfo ==3)
        {
            GetComponent<Renderer>().material.color = new Color32(245, 17, 17, 220);
        }
    }



    private void Start()
    {
        gen = TrainGame.Generator();
        wagon = transform.parent.GetComponentInChildren<Car>();
        IDD = rekord.Train.Id.ToString();
    }
    private void Update()
    {
        ColorTrain();
        AddLocomotivePosition();
        //TrainAudio();
        acceleration = 0.3f; //* gen.CompressionRate;
        decceleration = 0.7f;// * gen.CompressionRate;
        maxV = 1.8f;//* gen.CompressionRate;
        minV = 0.5f;// * gen.CompressionRate;
        trainPosition = this.transform.position;
        StatusSet();
        if (startingPoint != null && Go && !moveToStart)
        {
            MoveToStart();
            Heading = getHeading();
            lvNext = next.transform.position - lastVisited.transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, lvNext);
            moveToStart = true;
        }
        else if (startingPoint != null && Go && moveToStart)
        {
            if (!wagon.CarMoveToStart)
            {
                distanceFromLV = Vector3.Distance(transform.position, lastVisited.transform.position);
                if(distanceFromLV >=0.44) wagon.MoveToStart();
            }
            krytyczny = CriticalAngle();
            trainPosition = this.transform.position;
            toStop = StopWP();
            trainAngle = wagon.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, trainAngle);
            if (gen.CompressionRate <= 5)
            {
                realVelocity = Time.deltaTime * velocity * 1f;
                speedCorrection = wagon.SpeedCorrection(wagon.Distance);
                this.transform.position = Vector3.MoveTowards(this.transform.position, next.transform.position, realVelocity);
                if (wagon.CarMoveToStart) wagon.MoveCart(realVelocity+speedCorrection);
            }
            else
            {
                realVelocity = Time.deltaTime * velocity * 1f;
                speedCorrection = wagon.SpeedCorrection(wagon.Distance);
                this.transform.position = Vector3.MoveTowards(this.transform.position, next.transform.position, realVelocity);
                if (wagon.CarMoveToStart) wagon.MoveCart(realVelocity+speedCorrection);
            }

            if (trainPosition == next.transform.position)//ustawianie nexta i zmiana światła semaforu
            {

                if (next.GetComponent<PlatformTrack>() == null || next.GetComponent<PlatformTrack>().NextPoint == lastVisited)
                {
                    AssignNextPoint();
                }
                Heading = getHeading();
                if (lastVisited.GetComponent<Semaphore>() != null && next == lastVisited.GetComponent<Semaphore>().NextWaypoint)
                {
                    lastVisited.GetComponent<Semaphore>().Light.Toggle();
                }
            }
            if (distance < 0.5f && next.GetComponent<PlatformTrack>() != null && next.GetComponent<PlatformTrack>().NextPoint != lastVisited && velocity == 0)
            {
                if (!AssignedUsedPlatform)
                {
                    UsedPlatform = next.GetComponentInParent<PlatformGroup>().PlatformId;
                    AssignedUsedPlatform = true;
                    if (UsedPlatform == TrainRecord.PrimePeron && UsedPlatform == TrainRecord.Peron)
                    {
                        gen.Points += 3;
                        gen.AddScoreDialog(3, "Train " + IDD + " arrived at scheduled platform");
                    }
                    else if (UsedPlatform != TrainRecord.Peron)
                    {
                        gen.Points -= 2;
                        gen.AddScoreDialog(-2, "Train " + IDD + " arrived at wrong platform");
                    }
                }
                /* if (!EventRand && !gen.Emergency) Event();
                 if (Awaria || Choroba ) Go = false;*/
                Arrived = true;

                if (!ArrivalPoints)
                {
                    ArrivalHour = transform.parent.transform.parent.gameObject.GetComponent<TrainsTime>().gen.clock;
                    if (TrainRecord.ArrDelay <= 120)
                    {
                        gen.Points += 3;
                        gen.AddScoreDialog(3, "Train "+IDD+" arrived on time");
                    }
                    else if (TrainRecord.ArrDelay > 120)
                    {
                        gen.Points -= TrainRecord.ArrDelay / 60 - 2;
                        gen.AddScoreDialog(-(TrainRecord.ArrDelay / 60 - 2), "Train " + IDD + " arrived late");
                    }
                    ArrivalPoints = true;
                }
                if (!PlatformStand) timer += Time.deltaTime * gen.CompressionRate;
                if (timer > 60 && transform.parent.transform.parent.gameObject.GetComponent<TrainsTime>().gen.clock / 60 >= TrainRecord.DepartureTime / 60) PlatformStand = true;
                if (PlatformStand) timer = 0;
                if (next.GetComponent<PlatformTrack>().Wait == false && PlatformStand  /* && Awaria == false && Choroba ==false*/)
                {
                    Departed = true;
                    DepartureHour = transform.parent.transform.parent.gameObject.GetComponent<TrainsTime>().gen.clock;
                    AssignNextPoint();
                    if (TrainRecord.DepDelay > 120)
                    {
                        gen.Points -= TrainRecord.DepDelay / 60 - 2;
                        gen.AddScoreDialog(-(TrainRecord.DepDelay / 60 - 2), "Train " + IDD + " departed delayed");
                    }
                    else
                    {
                        gen.Points += 3;
                        gen.AddScoreDialog(3, "Train " + IDD + " departed on time");
                    }
                }
            }
            if (lastVisited.GetComponent<PlatformTrack>() != null && lastVisited.GetComponent<PlatformTrack>().NextPoint == next && velocity != 0 && PlatTimer < 2f)
            {
                PlatTimer += Time.deltaTime * gen.CompressionRate;
                if (PlatTimer > 1.5f)
                {

                    lastVisited.GetComponent<PlatformTrack>().Wait = true;
                    lastVisited.GetComponent<PlatformTrack>().Light.SetRed();
                }
            }
            DistancesCalculation();
            /*if (!TurningBack)*/ VelocityChange(); //to trzeba przemyśleć
            //else GoReverse(); wykomentowane cofanie pociagu
            if (velocity == 0 && next.GetComponent<PlatformTrack>() == null && PlatformStand)
            {
                HaltDelay += Time.deltaTime * gen.CompressionRate;
                if (HaltDelay >= 120f)
                {
                    HaltDelay = 0;
                    gen.Points -= 1;
                    gen.AddScoreDialog(-1, "Train " + IDD + " has been waiting 2 minutes");
                }
            }
            if (trainPosition == next.transform.position && next.End) //Kiedy pociąg dojeżdza do końca mapy
            {
                if (next.GetComponent<Waypoint>().EndRight == false)
                {
                    gen.Points -= 15;
                    gen.AddScoreDialog(-15, "Train " + IDD + " left map via left track");
                }
                Destroy(rekord.gameObject);
                RekordONChange.ResetNumber();
                gen.ScheduledTrains.Remove(TrainRecord);
                gen.ToFocusOn--;
                gen.RemaningTrains--;
                if (gen.RemaningTrains == 0) gen.GameOver(3);
                Destroy(gameObject);
            }

            JunctionCollisionCheck();
            SemaforCollisionCheck();
            if (Arrived == false && transform.parent.transform.parent.gameObject.GetComponent<TrainsTime>().gen.clock > TrainRecord.ArrivalTime)
            {
                ArrivalHour = transform.parent.transform.parent.gameObject.GetComponent<TrainsTime>().gen.clock;
            }
            if (Departed == false && transform.parent.transform.parent.gameObject.GetComponent<TrainsTime>().gen.clock > TrainRecord.DepartureTime)
            {
                DepartureHour = transform.parent.transform.parent.gameObject.GetComponent<TrainsTime>().gen.clock;
            }
            if (velocity == 0 && toStop.GetComponent<PlatformTrack>() == null && toStop.GetComponent<Semaphore>() == null && !JunctionStop)
            {
                gen.Points -= 3;
                gen.AddScoreDialog(-3, "Incorrectly set junction for train " + IDD);
                JunctionStop = true;
            }

            if (tempToStop != toStop)
            {
                JunctionStop = false;
            }
            tempToStop = toStop;

            
        
        }
        else if (startingPoint != null && !Go)
        {
            if (TrainRecord.ArrDelay > 0)
            {
                DelayWaitingTimer += Time.deltaTime*gen.CompressionRate;
                if (DelayWaitingTimer >= 480)
                {
                    gen.Points -= 5;
                    gen.AddScoreDialog(-5, "Delayed train " + IDD + " waiting approval");
                    DelayWaitingTimer = 0;
                }
            }
        }
       

        /* else if (startingPoint != null && !Go && moveToStart)
         {
             EventTimer += Time.deltaTime * gen.CompressionRate;
             if (Awaria && EventTimer >= 1200)
             {
                 Go = true;
                 Awaria = false;
                 gen.Emergency = false;
             }
             else if (Choroba && EventTimer >= 420)
             {
                 Go = true;
                 Choroba = false;
                 gen.Emergency = false;
             }
         }*/

    }
}
