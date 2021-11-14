using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Train : MonoBehaviour
{
    Waypoint CriticalAngle()
    {
        Waypoint wp1, wp2, wp3;        
        if (startingPoint != null)
        {
            wp2 = next;
            wp1 = lastVisited;
            wp3 = null;
            Vector3 katownik, katownik2;
            float angle = 0;
            if (wp2.Way2 == wp1) wp3 = wp2.Way1;
            else if (wp2.Way1 == wp1) wp3 = wp2.Way2;
            while (angle < 5 && wp3 != null)
            {
                katownik = wp2.transform.position - wp1.transform.position;
                katownik2 = wp3.transform.position - wp2.transform.position;
                angle = Vector3.Angle(katownik, katownik2);
                if (angle > 5) break;
                wp1 = wp2;
                wp2 = wp3;
                if (wp2.Way2 == wp1) wp3 = wp2.Way1;
                else if (wp2.Way1== wp1) wp3 = wp2.Way2;

            }
            if (angle > 5) return wp2;
            else return null;
        }
        return null;
}

}
