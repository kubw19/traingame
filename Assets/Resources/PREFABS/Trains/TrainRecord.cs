using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrainRecord : MonoBehaviour {
    public int Id;
    public int Peron;
    public int ArrivalTime;
    public int DepartureTime;
    public int ArrDelay;
    public int DepDelay;
    public string StartingPoint;
    public string EndingPoint;
    public Generator generator;
    [HideInInspector] public int TimetableBeg, TimetableEnd;
    public Train TrainUnit;
    public bool TrainEntryApproval;
    List<TrainRecord> ScheduledTrains;
    System.Random rand;
    List<int> UsedId;
    PeronFolder[] Platforms;
    public int PrimePeron;
    int RandDir;


    bool HourDirectionConflict()
    {
        foreach(TrainRecord Train in generator.ScheduledTrains)
        {
            if (Train.StartingPoint == StartingPoint && Math.Abs(Train.ArrivalTime - ArrivalTime) < 60) return true;
        }
        return false;
    }

    PeronFolder FreePeronSlot()
    {
        bool conflict = false;
        foreach (PeronFolder peron in Platforms)
        {
            conflict = false;
            if(!peron.AvailableEnds.Contains(TrainUnit.startingPoint) || !peron.AvailableEnds.Contains(TrainUnit.endingPoint))
            {
                conflict = true;
                continue;
            }
            foreach (TrainRecord element in peron.AssignedTrains)
            {
                if (!peron.DoublePlatform)
                {

                    if ((ArrivalTime <= element.DepartureTime && ArrivalTime >= element.ArrivalTime) || (DepartureTime <= element.DepartureTime && DepartureTime >= element.ArrivalTime) || (ArrivalTime <= element.ArrivalTime && DepartureTime >= element.DepartureTime))
                    {
                        conflict = true;
                        break;
                    }//jeśli jest konflikt to przechodzimy do następnego peronu
                }
                else
                {

                    if (((ArrivalTime <= element.DepartureTime && ArrivalTime >= element.ArrivalTime) || (DepartureTime <= element.DepartureTime && DepartureTime >= element.ArrivalTime) || (ArrivalTime <= element.ArrivalTime && DepartureTime >= element.DepartureTime)) && peron.OneDouble < 2) peron.OneDouble++;
                    if (((ArrivalTime <= element.DepartureTime && ArrivalTime >= element.ArrivalTime) || (DepartureTime <= element.DepartureTime && DepartureTime >= element.ArrivalTime) || (ArrivalTime <= element.ArrivalTime && DepartureTime >= element.DepartureTime)) && peron.OneDouble == 2)
                    {
                        peron.OneDouble = 0;
                        conflict = true;
                        break;
                    }
                }
            }
            peron.OneDouble = 0;
            if (!conflict) return peron;
        }
        return null;
    }

    void GenerateId()
    {
        int TempId = rand.Next(10000, 99999);
        if (UsedId.Count != 0)
        {
            while (UsedId.Contains(TempId)) TempId = rand.Next(10000, 99999);
        }
        UsedId.Add(TempId);
        Id = TempId;
    }

    void AssignTrain()
    {
        GameObject newTrain = Instantiate(Resources.Load("PREFABS/Trains/TrainSet"), GameObject.Find("Trains").transform, true) as GameObject;
        newTrain.name = "Set " + Id.ToString();
        TrainUnit = newTrain.transform.Find("trainTemplate").GetComponent<Train>();
        TrainUnit.TrainRecord = this;
    }

    void AssignStartingPoint()
    {
        RandDir = rand.Next(0, 20000) % 2;
        waypoint[] PKT = TrainUnit.GetComponentInParent<StartingPoints>().PunktyStartoweWest;
        if (RandDir == 0) PKT = TrainUnit.GetComponentInParent<StartingPoints>().PunktyStartoweEast;

        int SPAmount = PKT.Length;
        int los = rand.Next(0, 20000) % SPAmount;

        TrainUnit.startingPoint = PKT[los];
        StartingPoint = TrainUnit.startingPoint.Name;
    }

    void AssignEndingPoint()
    {
        waypoint[] PKT = TrainUnit.GetComponentInParent<StartingPoints>().PunktyKoncoweEast;
        if (RandDir == 0) PKT = TrainUnit.GetComponentInParent<StartingPoints>().PunktyKoncoweWest;

        int SPAmount = PKT.Length;
        int los = rand.Next(0, 20000) % SPAmount;

        while (PKT[los]==TrainUnit.startingPoint) los = rand.Next(0, 20000) % SPAmount;

        TrainUnit.endingPoint = PKT[los];
        EndingPoint = TrainUnit.endingPoint.Name;
    }

    void GenerateScheduleTime()
    {


        ArrivalTime = rand.Next(TimetableBeg, TimetableEnd);

        int los = rand.Next(1, 100);
        if (los <= 85) TrainUnit.StopTime = 120f;
        else if (los > 85 && los <= 95) TrainUnit.StopTime = 300f;
        else if (los > 95) TrainUnit.StopTime = 600f;
        DepartureTime = ArrivalTime + (int)TrainUnit.StopTime;
        PeronFolder ChoosenPeron;
        while ((ChoosenPeron = FreePeronSlot()) == null || HourDirectionConflict())
        {
            ArrivalTime+=60;
            DepartureTime+=60;
        }
        int i = 0;
        foreach (PeronFolder platform in Platforms)
        {
            if (platform.PlatformId == ChoosenPeron.PlatformId) break;
            i++;
        }
        Peron = ChoosenPeron.PlatformId;
        PrimePeron = Peron;
        Platforms[i].AssignedTrains.Add(this);
       

    }

    void CheckDelay()
    {
        if (TrainEntryApproval) TrainUnit.TrainEntryApproval();
        if (TrainUnit.ArrivalHour != 100000 && TrainUnit.Arrived == true) ArrDelay = TrainUnit.ArrivalHour - ArrivalTime;
        else ArrDelay = generator.clock - ArrivalTime;
        if (TrainUnit.DepartureHour != 100000 && TrainUnit.Departed == true) DepDelay = TrainUnit.DepartureHour - DepartureTime;
        else DepDelay = generator.clock - DepartureTime;
    }

    public void Prepare(Generator Gen)
    {
        ScheduledTrains = Gen.ScheduledTrains;
        rand = Gen.rand;
        UsedId = Gen.UsedId;
        Platforms = Gen.Platforms;
        TimetableBeg = Gen.TimetableBeg;
        TimetableEnd = Gen.TimetableEnd;
        GenerateId();
        generator = Gen;
        Gen.GetComponent<Generator>().ShufflePlatforms();
        AssignTrain();
        AssignStartingPoint();
        AssignEndingPoint();
        GenerateScheduleTime();
    }

    void FixedUpdate()
    {
        CheckDelay();
    }
}

