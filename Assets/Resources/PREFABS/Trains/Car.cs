using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public Train Locomotive;
    public Generator gener;
    public Waypoint NextWaypoint;
    public Waypoint LastVisited;
    Vector3 Offset = new Vector3(-0.45f,0,0);
    public bool CarMoveToStart = false;
    public float Distance;
    public float Velocity;
    private Vector3 carPosition;
    //public float previousDistance;
    public float deltaV;
    
    private void Start()
    {
        Locomotive = transform.parent.GetComponentInChildren<Train>();
        NextWaypoint = Locomotive.startingPoint;
        LastVisited = Locomotive.startingPoint;
        gener = TrainGame.Generator();
    }
    public void MoveToStart()
    {
        this.transform.position = Locomotive.lastVisited.transform.position;
        transform.rotation = Locomotive.transform.rotation;
        NextWaypoint = Locomotive.next;
        CarMoveToStart = true;

    }
    public float SpeedCorrection (float dist)
    {
        int compression;
        if (gener.CompressionRate < 2) compression = 1;
        else compression = 5;
        float deltaVV=0;
        if (Locomotive.velocity == 0) deltaVV = 0;
        else if (dist > 0.44f) deltaVV = 0.001f*compression;
        else if (dist < 0.44f) deltaVV = -0.001f*compression;
        return deltaVV;
    }
   
    void Update ()
    {
        carPosition = transform.position;
        if (CarMoveToStart)
        {
            transform.rotation = Locomotive.transform.rotation;
            Distance = Vector3.Distance(transform.position, Locomotive.transform.position);
            if (carPosition == NextWaypoint.transform.position)
            {
                NextWaypoint = Locomotive.next;
            }
            GetComponent<Renderer>().material.color = Locomotive.GetComponent<Renderer>().material.color;
        }
    }

    public void MoveCart(float VMov)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, NextWaypoint.transform.position, VMov);
    }
}
