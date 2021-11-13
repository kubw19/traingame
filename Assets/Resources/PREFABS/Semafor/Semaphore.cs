using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaphore : Waypoint
{
    /*
     * Klasa przyjmuję wartość następnego waypointa w którego stronę semafor będzie zezwalał lub nie zezwalał na wjazd
     * 
     */

    public Waypoint NextWaypoint;
    [HideInInspector] public SemaphoreLight Light;
    [HideInInspector] public bool Stop;
    private void Start()
    {
        Stop = true;
        Light = this.transform.GetChild(0).GetComponent<SemaphoreLight>();
        Light.SetRed();
    }

}
