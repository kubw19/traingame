using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteCreationMode : MonoBehaviour
{
    // Start is called before the first frame update

    public bool IsRouteCreationMode { get; set; } = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        IsRouteCreationMode = !IsRouteCreationMode;
    }
}
