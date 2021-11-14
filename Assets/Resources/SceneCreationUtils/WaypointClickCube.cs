using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointClickCube : MonoBehaviour
{
    public static bool _isOn = false;
    public static bool IsBuilderModeOn
    {
        get => _isOn;
        set
        {
            _isOn = value;
            if (value == true)
            {
                //SelectedWaypoint.GetComponentInChildren<WaypointClickCube>().GetComponent<MeshRenderer>().material.color = Color.white;
                //SelectedWaypoint = null;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    bool previousState = false;
    //private void Update()
    //{
    //    if(previousState != IsBuilderModeOn && !IsBuilderModeOn)
    //    {
    //        GetComponent<MeshRenderer>().material.color = Color.white;
    //    }
    //    previousState = IsBuilderModeOn;
    //    GetComponent<MeshRenderer>().enabled = IsBuilderModeOn;

    //}

    private static Waypoint SelectedWaypoint;


    public static void WaypointClick(Waypoint waypoint)
    {

        if (!IsBuilderModeOn || waypoint == null)
        {
            return;
        }
        if (SelectedWaypoint != null)
        {
            if(SelectedWaypoint == waypoint)
            {
                return;
            }
            SelectedWaypoint.GetComponentInChildren<WaypointClickCube>().GetComponent<MeshRenderer>().material.color = Color.white;

            if (!waypoint.Ways.Contains(SelectedWaypoint) && !SelectedWaypoint.Ways.Contains(waypoint))
            {
                waypoint.Ways.Add(SelectedWaypoint);

                SelectedWaypoint.Ways.Add(waypoint);

                var generator = GameObject.Find("Tracks").GetComponent<TracksGenerator>();
                //generator.Generate();

                Debug.Log($"CREATED ROUTE {waypoint.ObjectName} - {SelectedWaypoint.ObjectName}");

                SelectedWaypoint = null;
            }
        }
        else
        {
            Debug.Log($"SELECTED WAYPOINT {waypoint.ObjectName}");
            SelectedWaypoint = waypoint;
            waypoint.GetComponentInChildren<WaypointClickCube>().GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public Waypoint Waypoint => transform.parent.GetComponent<Waypoint>();

}
