using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Train : MonoBehaviour {

    /*
     * Funkcja zwraca punkt , w którym pociąg powinien się zatrzymać
     * 
     * */
    waypoint StopWP()
    {
        waypoint next2 = next;
        waypoint LV = lastVisited;
        if (startingPoint != null)
        {
            while (next2.ways[0] != LV || next2.ways[1] != null)
            {
                if (LV == next2.ways[0])
                {
                    LV = next2;
                    next2 = next2.ways[1];
                }
                else if (LV == next2.ways[1])
                {
                    LV = next2;
                    next2 = next2.ways[0];
                }

                //zatrzymanie przed semaforem
                if (LV.GetComponent<Semafor>() != null && next2==LV.GetComponent<Semafor>().NextWaypoint && LV.GetComponent<Semafor>().Stop)
                {
                    return LV;
                }

                //zatrzymanie na peronie
                if (LV.GetComponent<Peron>() != null && next2 == LV.GetComponent<Peron>().nextPoint && LV.GetComponent<Peron>().Wait)
                {
                    return LV;
                }
                if (LV.GetComponent<Peron>() != null && next2 == LV.GetComponent<Peron>().nextPoint && GetComponent<Train>().PlatformStand==false)
                {
                    return LV;
                }

            }
        }
        if (next2.End) return null;
        else return next2;
    }
}