using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semafor : MonoBehaviour
{
    /*
     * Klasa przyjmuję wartość następnego waypointa w którego stronę semafor będzie zezwalał lub nie zezwalał na wjazd
     * 
     */

    public waypoint NextWaypoint;
    [HideInInspector] public SemaforLight Light;
    [HideInInspector] public bool Stop;
    private void Start()
    {
        Stop = true;
        Light = this.transform.GetChild(0).GetComponent<SemaforLight>();
        Light.SetRed();
    }

}
