using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Train : MonoBehaviour
{

    /*
     * Funkcja zwraca punkt , w którym pociąg powinien się zatrzymać
     * 
     * */
    Waypoint StopWP()
    {
        Waypoint next2 = next;
        Waypoint lastVisitedWP = lastVisited;
        if (startingPoint != null)
        {
            while (next2.Ways[0] != lastVisitedWP && (next2.Ways.Count > 0))
            {
                if (lastVisitedWP == next2.Ways[0])
                {
                    lastVisitedWP = next2;
                    next2 = next2.Ways[1];
                }
                else if (lastVisitedWP == next2.Ways[1])
                {
                    lastVisitedWP = next2;
                    next2 = next2.Ways[0];
                }

                //zatrzymanie przed semaforem
                if (lastVisitedWP.GetComponent<Semaphore>() != null && next2 == lastVisitedWP.GetComponent<Semaphore>().NextWaypoint && lastVisitedWP.GetComponent<Semaphore>().Stop)
                {
                    return lastVisitedWP;
                }

                //zatrzymanie na peronie
                if (lastVisitedWP.GetComponent<PlatformTrack>() != null && next2 == lastVisitedWP.GetComponent<PlatformTrack>().NextPoint && lastVisitedWP.GetComponent<PlatformTrack>().Wait)
                {
                    return lastVisitedWP;
                }
                if (lastVisitedWP.GetComponent<PlatformTrack>() != null && next2 == lastVisitedWP.GetComponent<PlatformTrack>().NextPoint && GetComponent<Train>().PlatformStand == false)
                {
                    return lastVisitedWP;
                }


            }
        }
        if (next2.End)
        {
            return null;
        }
        else return null;//next2;
    }
}