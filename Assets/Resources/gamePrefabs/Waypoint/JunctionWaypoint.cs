using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class JunctionWaypoint : Waypoint
{
    public Waypoint AltWay;
    public void Toggle()
    {
        if (!transform.parent.GetComponent<JunctionFail>().Damaged)
        {
            var currentWay = Way2;
            var newWay = AltWay;
            Way2 = newWay;
            AltWay = currentWay;
        }
    }
}

