//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[ExecuteInEditMode]
//public class TrackGenerating : MonoBehaviour {


//    Vector3 WersorY = new Vector3(1, 0, 0);
//    Vector3 Katownik;
//    public Vector3 temp;
//    public float Angle;
//    // Use this for initialization
//    private void Awake()
//    {
//        foreach (Transform child in transform)
//        {
//            child.GetComponent<Waypoint>().TrackGenerated = false;

//        }
//    }
//    void Start () {
//		foreach(Transform child in transform)
//        {
//            if (!child.GetComponent<Waypoint>().TrackGenerated)
//            {
//                child.GetComponent<Waypoint>().TrackGenerated = true;
//                child.GetComponent<Waypoint>().ways[0].TrackGenerated = true;
//                Katownik = child.GetComponent<Waypoint>().ways[0].transform.position - child.transform.position;
//                Angle = Vector3.Angle(WersorY, Katownik);

//                GameObject newTrack = Instantiate(Resources.Load("PREFABS/Tor/TorTemplate")) as GameObject;
//                newTrack.transform.SetParent(GameObject.Find("Tory").transform, false);
//                if (child.GetComponent<Waypoint>().ways[0].transform.position.y < child.transform.position.y)
//                {
//                    newTrack.transform.Rotate(0, 0, -Angle);
//                }
//                else newTrack.transform.Rotate(0, 0, Angle);

//                newTrack.transform.position = child.transform.position + (Katownik / 2) + new Vector3(0, 0, 1);
//                temp = newTrack.transform.localScale;
//                temp.x = Vector2.Distance(child.transform.position, child.GetComponent<Waypoint>().ways[0].transform.position);
//                newTrack.transform.localScale = temp;
//                newTrack.name = child.name + "+" + child.GetComponent<Waypoint>().ways[0].name;




//                if (child.GetComponent<Waypoint>().ways[1] != null)
//                {
//                    child.GetComponent<Waypoint>().ways[1].TrackGenerated = true;
//                    Katownik = child.GetComponent<Waypoint>().ways[1].transform.position - child.transform.position;
//                    Angle = Vector3.Angle(WersorY, Katownik);

//                    newTrack = Instantiate(Resources.Load("PREFABS/Tor/TorTemplate")) as GameObject;
//                    newTrack.transform.SetParent(GameObject.Find("Tory").transform, false);
//                    if (child.GetComponent<Waypoint>().ways[1].transform.position.y < child.transform.position.y)
//                    {
//                        newTrack.transform.Rotate(0, 0, -Angle);
//                    }
//                    else newTrack.transform.Rotate(0, 0, Angle);

//                    newTrack.transform.position = child.transform.position + (Katownik / 2) + new Vector3(0, 0, 1);
//                    temp = newTrack.transform.localScale;
//                    temp.x = Vector2.Distance(child.transform.position, child.GetComponent<Waypoint>().ways[1].transform.position);
//                    newTrack.transform.localScale = temp;
//                    newTrack.name = child.name + "+" + child.GetComponent<Waypoint>().ways[1].name;
//                }
//            }
//        }
//	}
	
//	// Update is called once per frame

//}
