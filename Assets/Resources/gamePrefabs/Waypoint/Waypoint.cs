using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public Waypoint[] ways = new Waypoint[3];
    public bool End;
    public bool EndRight;
    public Train train;//zmienna do semaforu
    private Waypoint temp;
    public string Name;
    public bool TrackGenerated = false;
    public bool IsPlatform = false;

    public Waypoint GetWay(int index)
    {
        if(index + 1 > ways.Length)
        {
            return null;
        }

        return ways[index];
    }

    public void setGlobalPosition(Vector3 pozycja)
    {
        transform.position = pozycja;
    }
    void CleanUpSemaphoreTrain()
    {
        if (train != null && train.semafor != this) train = null;
    }

    public void Toggle(Waypoint junction)
    {
        if (!transform.parent.GetComponent<JunctionFail>().Damaged)
        {
            var currentWay = ways[1];
            var newWay = ways[2];
            ways[1] = newWay;
            ways[2] = currentWay;

            currentWay.ways = currentWay.ways.ToList().Where(x => x != junction).ToArray();

            var newWayList = newWay.ways.ToList();
            newWayList.Add(junction);

            newWay.ways = newWayList.ToArray();
        }
    }

    private void FixedUpdate()
    {
        CleanUpSemaphoreTrain();
    }
}
