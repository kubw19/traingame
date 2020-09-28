using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour
{

    public waypoint[] ways = new waypoint[3];
    public bool End;
    public bool EndRight;
    public Train train;//zmienna do semaforu
    private waypoint temp;
    public string Name;
    public bool TrackGenerated = false;
    public bool peron = false;

    public void setGlobalPosition(Vector3 pozycja)
    {
        transform.position = pozycja;
    }
    void CleanUpSemaphoreTrain()
    {
        if (train != null && train.semafor != this) train = null;
    }

    public void Toggle(waypoint junction)
    {
        if (!transform.parent.GetComponent<JunctionFail>().Damaged)
        {
            temp = ways[2];
            ways[2] = ways[1];
            ways[1] = temp;
            ways[1].ways[1] = junction;
            ways[2].ways[1] = null;
        }
    }

    private void FixedUpdate()
    {
        CleanUpSemaphoreTrain();
    }
}
