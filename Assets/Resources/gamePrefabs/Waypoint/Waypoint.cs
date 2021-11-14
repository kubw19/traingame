using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class Waypoint : MonoBehaviour
{

    public Waypoint Way1;
    public Waypoint Way2;
    public Train train;//zmienna do semaforu
    private Waypoint temp;
    public string Name;
    public string ObjectName => this.name;
    public bool TrackGenerated = false;
    public bool IsPlatform = false;


    public bool IsJunction => this is JunctionWaypoint;
    public bool IsMapEdge => this is EdgeWaypoint;

    private void Awake()
    {

    }

    public void AssignWay(Waypoint way)
    {
        if (Way1 == null)
        {
            Way1 = way;
            return;
        }
        Way2 = way;
    }

    public void UnnasignWay(Waypoint way)
    {
        if (Way1 == way)
        {
            Way1 = null;
        }
        if (Way2 == way)
        {
            Way2 = null;
        }
    }

    public bool CanGoTo(Waypoint waypoint)
    {
        return Way1 == waypoint || Way2 == waypoint;
    }

    public void setGlobalPosition(Vector3 pozycja)
    {
        transform.position = pozycja;
    }
    void CleanUpSemaphoreTrain()
    {
        if (train != null && train.semafor != this) train = null;
    }


    private void FixedUpdate()
    {
        CleanUpSemaphoreTrain();
    }


}
