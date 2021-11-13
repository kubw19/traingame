using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrack : MonoBehaviour
{

    public Waypoint NextPoint;
    [HideInInspector] public SemaphoreLight Light;
    [HideInInspector] public bool Wait;
    private void Start()
    {
        Light = this.transform.GetChild(0).GetComponent<SemaphoreLight>();
        Wait = true;
        Light.SetRed();
    }
}
