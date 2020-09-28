using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peron : MonoBehaviour
{

    public waypoint nextPoint;
    [HideInInspector] public PeronStop Light;
    [HideInInspector] public bool Wait;
    private void Start()
    {
        Light = this.transform.GetChild(0).GetComponent<PeronStop>();
        Wait = true;
        Light.SetRed();
    }
}
