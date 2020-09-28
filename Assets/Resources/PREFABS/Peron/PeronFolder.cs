using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeronFolder : MonoBehaviour {
    public int PlatformId;
    public List<TrainRecord> AssignedTrains = new List<TrainRecord>();
    public List<waypoint> AvailableEnds;
    [HideInInspector]public bool DoublePlatform;
    public int OneDouble = 0;

}
