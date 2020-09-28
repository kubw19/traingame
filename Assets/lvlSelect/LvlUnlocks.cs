using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using UnityEngine.SceneManagement;

public class LvlUnlocks : MonoBehaviour {

    public void backToMenu()
    {
        Destroy(GameObject.Find("AudioManager"));
        Destroy(GameObject.Find("FPS"));
        Destroy(GameObject.Find("HeartsCounterController"));
        Destroy(GameObject.Find("LevelPasser"));
        SceneManager.LoadScene("MainMenu");
    }

    public LevelsUnlocks [] Poziomy = new LevelsUnlocks[10];
    public int Stars;
    public bool DataLoaded = false;
    public int UnlockedLvls;
    Vector3 WersorY = new Vector3(1, 0, 0);
    Vector3 wektorOdleglosci;
    public List<stickPath> pathList = new List<stickPath>();
    float angle;

    int Increasement;

    int[] liczbaWkolumnie = new int[20];

    int distanceFromLevel0(LevelsUnlocks poziom)
    {
        if (poziom.Prev[0] != null) {
            foreach (LevelsUnlocks poprzednik in poziom.Prev)
                {
                    
                    if (poprzednik == Poziomy[0]) return 1;
                    else return distanceFromLevel0(poprzednik) + 1;
                }
        }
        return 0;
    }


    //funkcja tworząca plik z danymi poziomów gdy jeszcze nie został utworzony
    void FileNotExists()
    {
        Poziomy[0] = new LevelsUnlocks();
        Poziomy[1] = new LevelsUnlocks();
        Poziomy[2] = new LevelsUnlocks();
        Poziomy[3] = new LevelsUnlocks();
        Poziomy[4] = new LevelsUnlocks();
        Poziomy[5] = new LevelsUnlocks();
        Poziomy[6] = new LevelsUnlocks();
        Poziomy[7] = new LevelsUnlocks();
        Poziomy[8] = new LevelsUnlocks();
        Poziomy[9] = new LevelsUnlocks();


        Poziomy[0].Name = "L0";
        Poziomy[0].MinimalStarsAmount = 0;
        Poziomy[0].Locked = false;
        Poziomy[0].Prev = new LevelsUnlocks[1];
        Poziomy[0].Prev[0] = null;
        Poziomy[0].Next = new LevelsUnlocks[3];
        Poziomy[0].Next[0] = Poziomy[1];
        Poziomy[0].Next[1] = Poziomy[2];
        Poziomy[0].Next[2] = Poziomy[3];
        Poziomy[0].levelName = "zerowy";
        Poziomy[0].positionX = 0;
        Poziomy[0].positionY = 0;

        Poziomy[1].Name = "L0";
        Poziomy[1].MinimalStarsAmount = 2;
        Poziomy[1].Locked = true;
        Poziomy[1].Prev = new LevelsUnlocks[1];
        Poziomy[1].Prev[0] = Poziomy[0];
        Poziomy[1].Next = new LevelsUnlocks[1];
        Poziomy[1].Next[0] = Poziomy[4];
        Poziomy[1].levelName = "pierwszy";
        Poziomy[1].positionX = 1;
        Poziomy[1].positionY = 1;



        Poziomy[2].Name = "L0";
        Poziomy[2].MinimalStarsAmount = 2;
        Poziomy[2].Locked = true;
        Poziomy[2].Prev = new LevelsUnlocks[1];
        Poziomy[2].Prev[0] = Poziomy[0];
        Poziomy[2].Next = new LevelsUnlocks[2];
        Poziomy[2].Next[0] = Poziomy[4];
        Poziomy[2].Next[1] = Poziomy[8];
        Poziomy[2].levelName = "drugi";
        Poziomy[2].positionX = 1;
        Poziomy[2].positionY = 0;

        Poziomy[3].Name = "L0";
        Poziomy[3].MinimalStarsAmount = 2;
        Poziomy[3].Locked = true;
        Poziomy[3].Prev = new LevelsUnlocks[1];
        Poziomy[3].Prev[0] = Poziomy[0];
        Poziomy[3].Next = new LevelsUnlocks[1];
        Poziomy[3].Next[0] = Poziomy[5];
        Poziomy[3].levelName = "trzeci";
        Poziomy[3].positionX = 1;
        Poziomy[3].positionY = -1;

        Poziomy[4].Name = "L0";
        Poziomy[4].MinimalStarsAmount = 2;
        Poziomy[4].Locked = true;
        Poziomy[4].Prev = new LevelsUnlocks[2];
        Poziomy[4].Prev[0] = Poziomy[1];
        Poziomy[4].Prev[1] = Poziomy[2];
        Poziomy[4].Next = new LevelsUnlocks[2];
        Poziomy[4].Next[0] = Poziomy[6];
        Poziomy[4].Next[1] = Poziomy[9];
        Poziomy[4].levelName = "czwarty";
        Poziomy[4].positionX = 2;
        Poziomy[4].positionY = 1;


        Poziomy[5].Name = "L0";
        Poziomy[5].MinimalStarsAmount = 2;
        Poziomy[5].Locked = true;
        Poziomy[5].Prev = new LevelsUnlocks[1];
        Poziomy[5].Prev[0] = Poziomy[3];
        Poziomy[5].Next = new LevelsUnlocks[1]; ;
        Poziomy[5].Next[0] = Poziomy[8];
        Poziomy[5].levelName = "piaty";
        Poziomy[5].positionX = 2;
        Poziomy[5].positionY = -1;

        Poziomy[6].Name = "L0";
        Poziomy[6].MinimalStarsAmount = 2;
        Poziomy[6].Locked = true;
        Poziomy[6].Prev = new LevelsUnlocks[1];
        Poziomy[6].Prev[0] = Poziomy[4];
        Poziomy[6].Next = new LevelsUnlocks[2];
        Poziomy[6].Next[0] = Poziomy[7];
        Poziomy[6].Next[1] = Poziomy[8];
        Poziomy[6].levelName = "trzeci i pol";
        Poziomy[6].positionX = 3;
        Poziomy[6].positionY = 1;

        Poziomy[7].Name = "L0";
        Poziomy[7].MinimalStarsAmount = 2;
        Poziomy[7].Locked = true;
        Poziomy[7].Prev = new LevelsUnlocks[1];
        Poziomy[7].Prev[0] = Poziomy[6];
        Poziomy[7].Next = null;
        Poziomy[7].levelName = "siodmy";
        Poziomy[7].positionX = 4;
        Poziomy[7].positionY = 1;

        Poziomy[8].Name = "L0";
        Poziomy[8].MinimalStarsAmount = 2;
        Poziomy[8].Locked = true;
        Poziomy[8].Prev = new LevelsUnlocks[3];
        Poziomy[8].Prev[0] = Poziomy[6];
        Poziomy[8].Prev[1] = Poziomy[2];
        Poziomy[8].Prev[2] = Poziomy[5];
        Poziomy[8].Next = null;
        Poziomy[8].levelName = "osmy";
        Poziomy[8].positionX = 4;
        Poziomy[8].positionY = 0;

        Poziomy[9].Name = "L0";
        Poziomy[9].MinimalStarsAmount = 2;
        Poziomy[9].Locked = true;
        Poziomy[9].Prev = new LevelsUnlocks[1];
        Poziomy[9].Prev[0] = Poziomy[4];
        Poziomy[9].Next = null;
        Poziomy[9].levelName = "dziewiaty";
        Poziomy[9].positionX = 3;
        Poziomy[9].positionY = 2;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream LevelsData = File.Open(Application.persistentDataPath + "/LevelsData.jmw", FileMode.Create);
        bf.Serialize(LevelsData, Poziomy);

        LevelsData.Close();
    }


    //funkcja otwierająca plik z danymi poziomów
    void LoadLevelData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream LevelsData = File.Open(Application.persistentDataPath + "/LevelsData.jmw", FileMode.Open);
        Poziomy = (LevelsUnlocks[])bf.Deserialize(LevelsData);
        LevelsData.Close();
    }


    //funkcja aktualizująca plik z danymi poziomów
    public void UpdateLevelData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream LevelsData = File.Open(Application.persistentDataPath + "/LevelsData.jmw", FileMode.Create);
        bf.Serialize(LevelsData, Poziomy);
        LevelsData.Close();
    }



    public void IncreaseMinimumStars()//zwiększenie minimalnej liczby gwiazdek dla każdego poziomu
    {

        if (UnlockedLvls == 1) Increasement = 2;
        else if (UnlockedLvls == 2 || UnlockedLvls == 3 || UnlockedLvls == 4 || UnlockedLvls == 5) Increasement = 3;
        else Increasement = 4;
        UnlockedLvls++;
        PlayerPrefs.SetInt("Unlocked Levels", UnlockedLvls);
        foreach (LevelsUnlocks Lvl in Poziomy)
        {
           
            if(Lvl.Locked)Lvl.MinimalStarsAmount += Increasement;
            
        }
    }

    
    //funkcja zwracająca id danego poziomu w tablicy z poziomami
    int idPoziomu(LevelsUnlocks poziom)
    {
        for (int i = 0; i < Poziomy.Length; i++)
        {
            if (Poziomy[i] == poziom) return i;
        }
        return -1;
    }

    //wykrywanie kolizji ścieżek między blokami
    bool blockPathCollision(stickPath path)
    {
        for(int i = 1; i<path.waypoints.Count - 1; i++)
        {
            foreach(LevelsUnlocks poziom in Poziomy)
            {
                //przejście z Vector3 na Vector2
                Vector2 blockPosition = new Vector2();
                blockPosition.x = poziom.block.transform.localPosition.x;
                blockPosition.y = poziom.block.transform.localPosition.y;

                if (blockPosition == path.waypoints[i].waypoint && !path.from.Next.Contains(poziom))
                {
                    return true;
                }
            }
        }

        return false;
    }

    //funkcja wstawiająca kreske łączącą dwa bloki poziomów 
    void insertStick(Vector2 stickStartVector, Vector2 stickEndVector, string name)
    {
        //utworzenie kreski łączącej dwa poziomy
        GameObject newBlockStick = Instantiate(Resources.Load("PREFABS/blockStick"), GameObject.Find("Bloki").transform) as GameObject;
        newBlockStick.name = name;
        newBlockStick.transform.localPosition = stickStartVector;
        
        //obliczenie kąta między poziomami
        wektorOdleglosci = stickEndVector - stickStartVector;
        angle = Vector3.Angle(WersorY, wektorOdleglosci);


        //obrócenie kreski o wyznaczony wcześniej kąt między poziomami
        if (stickEndVector.y < stickStartVector.y)
        {
            newBlockStick.transform.Rotate(0, 0, -angle);
        }
        else
        {
            newBlockStick.transform.Rotate(0, 0, angle);
        }

        //przesunięcie kreski
        newBlockStick.transform.localPosition += wektorOdleglosci / 2;

        //wydłużenie kreski
        wektorOdleglosci = newBlockStick.transform.localScale;
        wektorOdleglosci.x = Vector2.Distance(stickStartVector, stickEndVector);
        newBlockStick.transform.localScale = wektorOdleglosci;
        //Debug.Log(wektorOdleglosci.x + " i " + stickEndTransform.position);

        
    }

    //funkcja wstawiająca zakręt 90 stopni
    void insertTurn(Vector2 begPosition, float direction, int orienation)
    {
        GameObject newStickTurn = Instantiate(Resources.Load("PREFABS/turn90"), GameObject.Find("Bloki").transform) as GameObject;
        newStickTurn.transform.localPosition = begPosition;

        if (orienation == -1)//jeśli w dół
        {
            newStickTurn.transform.Rotate(Vector3.right * 180);
        }

        if(direction == -1)
        {
            newStickTurn.transform.Rotate(Vector3.forward * -180);
        }

    }

    void insertLevelBlocks()
    {

        //pętla stawiająca bloki poziomów
        GameObject newBlock = null;
        Vector3 przesuniecie = Vector3.zero;
        foreach (LevelsUnlocks blokPoziomu in Poziomy)
        {
            newBlock = Instantiate(Resources.Load("PREFABS/LevelBlock"), GameObject.Find("Bloki").transform) as GameObject;

            //przekazanie danych poziomu bloku do komponentu odpowiedzialnego za wyświetlanie tekstu i grafiki
            newBlock.GetComponent<LvlId>().levelName = blokPoziomu.levelName;
            newBlock.GetComponent<LvlId>().Id = idPoziomu(blokPoziomu);
            newBlock.GetComponent<LvlId>().minimalStarsAmount = blokPoziomu.MinimalStarsAmount;

            newBlock.name = blokPoziomu.levelName;
            przesuniecie.x = blokPoziomu.positionX * 5;
            przesuniecie.y = blokPoziomu.positionY * 3;
            newBlock.transform.Translate(przesuniecie, Space.World);//przesunięcie bloku     

            blokPoziomu.block = newBlock; //przypisanie do listy poziomów nowo utworzony blok

           // blokPoziomu.trainQueue = new Queue<levelSelectTrain>();
           }

        //dodanie do listy ścieżek każdej ścieżki między blokami (bez kątów prostych)
        foreach (LevelsUnlocks blokPoziomu in Poziomy)
        {
            if (blokPoziomu.Next != null)
                foreach (LevelsUnlocks nastepnik in blokPoziomu.Next)
                {
                    stickPath path = new stickPath();
                    path.from = blokPoziomu;
                    path.to = nastepnik;

                    List<stickPathWaypoint> waypoints = new List<stickPathWaypoint>();

                    Vector2 waypointVecStart = new Vector2(0, 0);
                    Vector2 waypointVecEnd = new Vector2(0, 0);

                    stickPathWaypoint start = new stickPathWaypoint();
                    waypointVecStart.x = blokPoziomu.block.transform.localPosition.x;
                    waypointVecStart.y = blokPoziomu.block.transform.localPosition.y;
                    start.waypoint = waypointVecStart;

                    stickPathWaypoint end = new stickPathWaypoint();
                    waypointVecEnd.x = nastepnik.block.transform.localPosition.x;
                    waypointVecEnd.y = nastepnik.block.transform.localPosition.y;
                    end.waypoint = waypointVecEnd;

                    waypoints.Add(start);
                    waypoints.Add(end);

                    path.waypoints = waypoints;

                    pathList.Add(path);
                }
        }


        //miejsce na kod przerabiający ścieżki na ścieżki z kątami prostymi
        foreach(stickPath path in pathList)
        {
            //obliczenie kąta między poziomami
            wektorOdleglosci = path.from.block.transform.position - path.to.block.transform.position;
            angle = Vector2.Angle(WersorY, wektorOdleglosci);

            if(angle != 0 && angle != 180)
            {
                stickPathWaypoint posr = new stickPathWaypoint(true, angle);

                Vector2 posrednik = new Vector2();

                posrednik.x = path.from.block.transform.localPosition.x;
                posrednik.y = path.to.block.transform.localPosition.y;
                posr.waypoint = posrednik;

                path.waypoints.Insert(1, posr);

                if (blockPathCollision(path))
                {
                    path.waypoints[1].waypoint.y = path.from.block.transform.localPosition.y;
                    path.waypoints[1].waypoint.x = path.to.block.transform.localPosition.x;
                }
            }
        }


   
        //wstawienie zakrętów 90 stopni
        Vector2 offsetPoint;
        foreach (stickPath sp in pathList)//wstawienie kresek dla każdej ścieżki na mapie
        {
            for(int i = 0; i < sp.waypoints.Count - 1; i++)
            {
                if (sp.waypoints[i + 1].rightAngle)//ścieżka dochodząca do kąta prostego
                {
                    offsetPoint = new Vector2(0, 0);
                    offsetPoint += sp.waypoints[i+1].waypoint;
                    int direction = 1;
                    if (offsetPoint.x == sp.waypoints[i].waypoint.x && offsetPoint.y < sp.waypoints[i].waypoint.y)//jeśli ścieżka jest pionowa i idzie w dół
                        offsetPoint.y += 23;
                    else if (offsetPoint.x == sp.waypoints[i].waypoint.x && offsetPoint.y >= sp.waypoints[i].waypoint.y) //jeśli ścieżka jest pionowa i idzie w góre
                        offsetPoint.y -= 23;
                    else if (offsetPoint.y == sp.waypoints[i].waypoint.y)//jeśli ścieżka jest pozioma
                    {
                        offsetPoint.x -= 23;
                        direction = -1;
                    }

                    insertStick(sp.waypoints[i].waypoint, offsetPoint, "nazwa");

                    int orientation = 1;
                    if (sp.waypoints[i + 1].waypoint.y < sp.waypoints[i].waypoint.y) orientation = -1;

                    insertTurn(sp.waypoints[i+1].waypoint, direction, orientation);
                }
                else if (sp.waypoints[i].rightAngle)//ścieżka odchodząca od kąta prostego.
                {
                    offsetPoint = new Vector2(0,0);
                    offsetPoint += sp.waypoints[i].waypoint;

                    if (offsetPoint.x == sp.waypoints[i + 1].waypoint.x && offsetPoint.y < sp.waypoints[i + 1].waypoint.y)//jeśli ścieżka jest pionowa i idzie w gore
                        offsetPoint.y += 23;
                    else if (offsetPoint.x == sp.waypoints[i + 1].waypoint.x && offsetPoint.y >= sp.waypoints[i + 1].waypoint.y)//jeśli ścieżka jest pionowa i idzie w dol
                        offsetPoint.y -= 23;
                    else if (offsetPoint.y == sp.waypoints[i + 1].waypoint.y)//jeśli ścieżka jest pozioma
                        offsetPoint.x += 23;

                    insertStick(offsetPoint, sp.waypoints[i+1].waypoint, "nazwa");
                }
                else insertStick(sp.waypoints[i].waypoint, sp.waypoints[i+1].waypoint, "nazwa");
            }
        }


        //utworzenie listy dostępnych kierunków (fizycznych) dla każdego bloku poziomu
        foreach (stickPath path in pathList)
        {
            if (path.from.availableDirections == null) path.from.availableDirections = new List<blockDirection>();

            if (!path.from.availableDirections.Any(n => n.direction == Vector3.Normalize(path.waypoints[1].waypoint - path.waypoints[0].waypoint)))
            {
                blockDirection dir = new blockDirection();
                dir.direction = Vector3.Normalize(path.waypoints[1].waypoint - path.waypoints[0].waypoint);
                dir.paths = new Queue<stickPath>();
                dir.paths.Enqueue(path);
                path.from.availableDirections.Add(dir);
            }
            else
            {
                path.from.availableDirections.Find(n => n.direction == Vector3.Normalize(path.waypoints[1].waypoint - path.waypoints[0].waypoint)).paths.Enqueue(path);
            }
        }

        //przerobienie zakrętów 90 stopni na łuki
        for(int i = 0; i < pathList.Count; i++)
        {
            float degrees = 180;
            float arcDir = 1;
            float offsetX = 23;
            float offsetY = 23;
            stickPath path = pathList[i];
            if (path.waypoints.Find(n => n.rightAngle == true) != null) //jeśli ścieżka ma jakiś kąt prosty
            {
                List<stickPathWaypoint> newWaypointList = new List<stickPathWaypoint>();
                newWaypointList.Add(path.waypoints[0]);
                stickPathWaypoint beforeTurnWaypoint = new stickPathWaypoint();
                stickPathWaypoint afterTurnWaypoint = new stickPathWaypoint() ;

                if (path.waypoints[0].waypoint.x == path.waypoints[1].waypoint.x)//jeśli pionowa
                {
                    offsetY = 0;
                    if ((path.waypoints[1].waypoint - path.waypoints[0].waypoint).y >= 0)//jeśli w górę
                    {
                        beforeTurnWaypoint.waypoint = path.waypoints[1].waypoint - new Vector2(0, 23);
                        arcDir = -1;
                    }
                    else //jeśli w dół
                    {
                        beforeTurnWaypoint.waypoint = path.waypoints[1].waypoint - new Vector2(0, -23);
                        arcDir = 1;
                    }

                    afterTurnWaypoint.waypoint = path.waypoints[1].waypoint + new Vector2(23, 0);
                }
                else if (path.waypoints[0].waypoint.y == path.waypoints[1].waypoint.y)//jeśli pozioma (zawsze w prawo)
                {
                    offsetX = 0;
                    beforeTurnWaypoint.waypoint = path.waypoints[1].waypoint - new Vector2(23, 0);

                    if ((path.waypoints[2].waypoint - path.waypoints[1].waypoint).y >= 0)//jeśli w górę
                    {
                        afterTurnWaypoint.waypoint = path.waypoints[1].waypoint + new Vector2(0, 23);
                        degrees = 270;
                        arcDir = 1;
                    }
                    else //jeśli w dół
                    {
                        afterTurnWaypoint.waypoint = path.waypoints[1].waypoint - new Vector2(0, 23);
                        degrees = 270;
                        arcDir = -1;
                    }
                }

                newWaypointList.Add(beforeTurnWaypoint);

                //utworzenie łuku z punktów

                float j = degrees;
                while (j != degrees + 90 && j != degrees - 90) 
                {
                    stickPathWaypoint punkt = new stickPathWaypoint();
                    punkt.waypoint = new Vector2(offsetX+beforeTurnWaypoint.waypoint.x + 23 * Mathf.Cos(j*Mathf.Deg2Rad), offsetY + beforeTurnWaypoint.waypoint.y + 23 * Mathf.Sin(j*Mathf.Deg2Rad));
                    newWaypointList.Add(punkt);
                    j += arcDir*18;
                }

                newWaypointList.Add(afterTurnWaypoint);
                newWaypointList.Add(path.waypoints[2]);

                pathList[i].waypoints = newWaypointList;

            }
        }

    }


    private void Start()
    {
       // PlayerPrefs.DeleteAll();
        for (int i = -1; i > -2; i--)
        {
            Stars += PlayerPrefs.GetInt(i.ToString(), 0);
        }

        if (!File.Exists(Application.persistentDataPath + "/LevelsData.jmw"))
        {
            FileNotExists();
        }
        else
        {
            
            LoadLevelData();
        }
        UnlockedLvls = PlayerPrefs.GetInt("Unlocked Levels",1);
        DataLoaded = true;

        insertLevelBlocks();//rysowanie drzewa poziomow
        this.GetComponent<levelSelectTrainGenerator>().loadLevelSelectTrains();//zaladowanie pociagow
        //PlayerPrefs.DeleteAll();

    }

}

[Serializable]
public class LevelsUnlocks
{
    public string Name;
    public int MinimalStarsAmount;
    public bool Locked;
    public LevelsUnlocks [] Prev;
    public LevelsUnlocks [] Next;
    public string levelName;
    public float positionX;
    public float positionY;


    [NonSerialized]
    public GameObject block;

    [NonSerialized]
    public List<blockDirection> availableDirections;


}

public class blockDirection
{
    public Vector3 direction;
    public Queue<stickPath> paths;
}

public class stickPath
{
    public LevelsUnlocks from;
    public LevelsUnlocks to;
    public List<stickPathWaypoint> waypoints;

}

public class stickPathWaypoint
{
    public Vector2 waypoint;
    public bool rightAngle;
    public float direction;

    public stickPathWaypoint()
    {
        rightAngle = false;
    }

    public stickPathWaypoint(bool rightAngle, float direction)
    {
        this.rightAngle = rightAngle;
        this.direction = direction;
    }
}

