using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TrainRecord : MonoBehaviour
{
    public int Id;
    public int? Peron;
    public int ArrivalTime;
    public int DepartureTime;
    public int ArrDelay;
    public int DepDelay;
    public Generator generator;
    [HideInInspector] public int TimetableBeg, TimetableEnd;
    public Train TrainUnit;
    public bool TrainEntryApproval;
    List<TrainRecord> ScheduledTrains;
    System.Random rand;
    List<int> UsedId;
    PlatformGroup[] Platforms;
    public int? PrimePeron;
    int RandDir;
    private bool _startsFromLeft;


    public string StartingPoint => TrainUnit.startingPoint.name;
    public string EndingPoint => TrainUnit.endingPoint.name;

    bool HourDirectionConflict()
    {
        foreach (TrainRecord Train in generator.ScheduledTrains)
        {
            if (Train.StartingPoint == StartingPoint && Math.Abs(Train.ArrivalTime - ArrivalTime) < 60) return true;
        }
        return false;
    }


    private bool NonePlatformsAdded() => Platforms.Length == 0;

    PlatformGroup FreePeronSlot()
    {
        bool conflict = false;
        foreach (PlatformGroup peron in Platforms)
        {
            conflict = false;
            if (!peron.AvailableEnds.Contains(TrainUnit.startingPoint) || !peron.AvailableEnds.Contains(TrainUnit.endingPoint))
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
        var fromLeft = FindObjectsOfType<LeftEdgeWaypoint>().Where(x => x.CanTrainsBegin).Select(x => x as EdgeWaypoint).ToList();
        var fromRight = FindObjectsOfType<RightEdgeWaypoint>().Where(x => x.CanTrainsBegin).Select(x => x as EdgeWaypoint).ToList();
        RandDir = rand.Next(0, 20000) % 2;

        var points = fromRight;
        _startsFromLeft = false;
        if (RandDir == 0 && fromLeft.Count() > 0 || fromRight.Count() == 0)
        {
            _startsFromLeft = true;
            points = fromLeft;
        }

        int startingPointsAmount = points.Count();
        int selected = rand.Next(0, 20000) % startingPointsAmount;

        TrainUnit.startingPoint = points.ElementAt(selected);
    }

    void AssignEndingPoint()
    {
        var endPoints = (_startsFromLeft ? FindObjectsOfType<RightEdgeWaypoint>().Select(x => x as EdgeWaypoint) : FindObjectsOfType<LeftEdgeWaypoint>().Select(x => x as EdgeWaypoint)).Where(x => x.CanTrainsEnd).ToList();

        int startingPointsCount = endPoints.Count();
        int selected = rand.Next(0, 20000) % startingPointsCount;

        TrainUnit.endingPoint = endPoints.ElementAt(selected);
    }

    void GenerateScheduleTime()
    {


        ArrivalTime = rand.Next(TimetableBeg, TimetableEnd);

        int los = rand.Next(1, 100);
        if (los <= 85) TrainUnit.StopTime = 120f;
        else if (los > 85 && los <= 95) TrainUnit.StopTime = 300f;
        else if (los > 95) TrainUnit.StopTime = 600f;
        DepartureTime = ArrivalTime + (int)TrainUnit.StopTime;

        if (NonePlatformsAdded())
        {
            return;
        }
        PlatformGroup chosenPlatform;
        while ((chosenPlatform = FreePeronSlot()) == null || HourDirectionConflict())
        {
            ArrivalTime += 60;
            DepartureTime += 60;
        }

        Peron = chosenPlatform.PlatformId;
        PrimePeron = Peron;
        chosenPlatform.AssignedTrains.Add(this);


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

