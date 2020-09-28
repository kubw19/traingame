using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class DisplayingWordCoordinates : MonoBehaviour
{
    public Vector3 coordinates;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coordinates = this.transform.position;
    }
}
