using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MenuWaypointTrackGen : MonoBehaviour {

    // Use this for initialization
    Vector3 WersorY = new Vector3(1, 0, 0);
    Vector3 Katownik;
    public float Angle;

    void Start()
    {
        if (!GameObject.Find(this.name + "Track"))
        {
            Katownik = GetComponent<waypoint>().ways[0].transform.position - transform.position;
            Angle = Vector3.Angle(WersorY, Katownik);

            GameObject newTrack = Instantiate(Resources.Load("PREFABS/MenuTrack")) as GameObject;
            newTrack.transform.SetParent(GameObject.Find("Cube").transform, false);
            newTrack.transform.Rotate(0, 0, -Angle);
            newTrack.transform.position = transform.position + (Katownik / 2) + new Vector3(0, 0, 1);
            newTrack.name = this.name + "Track";
        }

    }	
}
