using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGroup : MonoBehaviour {
    public int PlatformId;
    public List<TrainRecord> AssignedTrains = new List<TrainRecord>();
    public List<Waypoint> AvailableEnds;
    [HideInInspector]public bool DoublePlatform;
    public int OneDouble = 0;

}
