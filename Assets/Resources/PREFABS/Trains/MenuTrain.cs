using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuTrain : MonoBehaviour {

    System.Random Los = new System.Random();
    public Waypoint startingpoint;
    Waypoint Selected;

    private void Start()
    {
        var waypoints = GameObject.FindObjectsOfType<Waypoint>();

        Selected = waypoints[Los.Next(0, 99000) % waypoints.Length];
        int iterator = 0;
        while (Selected.End && Selected.Ways[0].End && iterator<waypoints.Length)
        {
            iterator++;
            Selected = waypoints[Los.Next(0, 99000) % waypoints.Length];
        }

        startingpoint = Selected;
        Selected.End = true;
        Selected.Ways[0].End = true ;
        transform.position = startingpoint.transform.position;
    }
    private void Update()
    {
        if(startingpoint!=null)
        transform.position = Vector3.MoveTowards(this.transform.position, startingpoint.Ways[0].transform.position, Time.deltaTime*15);

        if (transform.position == startingpoint.Ways[0].transform.position)
        {
            Selected.End = false;
            Selected.Ways[0].End = false;

            Destroy(gameObject);
        }
    }
}