using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Train : MonoBehaviour
{

    /*
     * Funkcja zwraca punkt , w którym pociąg powinien się zatrzymać
     * 
     * */
    Waypoint NextWaypointToStop()
    {
        Waypoint iterator = next;
        List<Waypoint> visited = new List<Waypoint>() { lastVisited};
        if (startingPoint != null)
        {
            while (!iterator.IsMapEdge)
            {
                //zatrzymanie przed semaforem
                if (iterator.GetComponent<Semaphore>() != null && iterator == iterator.GetComponent<Semaphore>().NextWaypoint && iterator.GetComponent<Semaphore>().Stop)
                {
                    return iterator;
                }

                //zatrzymanie na peronie
                if (iterator.GetComponent<PlatformTrack>() != null && iterator == iterator.GetComponent<PlatformTrack>().NextPoint && iterator.GetComponent<PlatformTrack>().Wait)
                {
                    return iterator;
                }
                if (iterator.GetComponent<PlatformTrack>() != null && iterator == iterator.GetComponent<PlatformTrack>().NextPoint && GetComponent<Train>().PlatformStand == false)
                {
                    return iterator;
                }

                var possibleWays = new List<Waypoint> { iterator.Way1, iterator.Way2 };
                visited.Add(iterator);
                iterator = possibleWays.FirstOrDefault(x => !visited.Contains(x));

            }
        }
         return iterator.IsMapEdge ? null : iterator;
    }
}