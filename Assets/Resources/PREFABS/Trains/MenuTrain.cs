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
        while (Selected.IsMapEdge && Selected.Way1.IsMapEdge && iterator<waypoints.Length)
        {
            iterator++;
            Selected = waypoints[Los.Next(0, 99000) % waypoints.Length];
        }

        startingpoint = Selected;
        //Selected.IsMapEdge = true;
        //Selected.Ways[0].IsMapEdge = true ;
        transform.position = startingpoint.transform.position;
    }
    private void Update()
    {
        if(startingpoint!=null)
        transform.position = Vector3.MoveTowards(this.transform.position, startingpoint.Way1.transform.position, Time.deltaTime*15);

        if (transform.position == startingpoint.Way1.transform.position)
        {
            //Selected.IsMapEdge = false;
            //Selected.Ways[0].IsMapEdge = false;

            Destroy(gameObject);
        }
    }
}