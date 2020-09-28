using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuTrain : MonoBehaviour {

    System.Random Los = new System.Random();
    public waypoint startingpoint;
    waypoint Selected;

    private void Start()
    {
        var Waypointy = GameObject.FindObjectsOfType<waypoint>();

        Selected = Waypointy[Los.Next(0, 99000) % Waypointy.Length];
        int iterator = 0;
        while (Selected.End && Selected.ways[0].End && iterator<Waypointy.Length)
        {
            iterator++;
            Selected = Waypointy[Los.Next(0, 99000) % Waypointy.Length];
        }

        startingpoint = Selected;
        Selected.End = true;
        Selected.ways[0].End = true ;
        transform.position = startingpoint.transform.position;
    }
    private void Update()
    {
        if(startingpoint!=null)
        transform.position = Vector3.MoveTowards(this.transform.position, startingpoint.ways[0].transform.position, Time.deltaTime*15);

        if (transform.position == startingpoint.ways[0].transform.position)
        {
            Selected.End = false;
            Selected.ways[0].End = false;

            Destroy(gameObject);
        }
    }
}