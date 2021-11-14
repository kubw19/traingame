
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TracksGenerator : MonoBehaviour
{

    Vector3 _wersorY = new Vector3(1, 0, 0);
    Vector3 _katownik;
    public Vector3 temp;
    public float Angle;
    // Use this for initialization
    void Awake()//Ustawianie domyślnych wartości wygenerowania toru dla punktu na false - na razie nieużywane
    {
        //Debug.Log("Editor causes this Update");
        foreach (Waypoint child in UnityEngine.Object.FindObjectsOfType<Waypoint>())
        {
            child.TrackGenerated = false;
        }
    }

    public void Generate(Waypoint from, Waypoint to)
    {
        _katownik = to.transform.position - from.transform.position;
        Angle = Vector3.Angle(_wersorY, _katownik);

        var s1 = from.name + "+" + to.name;
        var s2 = to.name + "+" + from.name;

        if (GameObject.Find(s1) != null || GameObject.Find(s2) != null)
        {
            return;
        }

        GameObject newTrack = Instantiate(Resources.Load("gamePrefabs/Rail/Rail")) as GameObject;
        newTrack.transform.SetParent(GameObject.Find("Rails").transform, false);

        if (to.transform.position.y < from.transform.position.y)
        {
            newTrack.transform.Rotate(0, 0, -Angle);
        }
        else newTrack.transform.Rotate(0, 0, Angle);

        newTrack.transform.position = from.transform.position + (_katownik / 2) + new Vector3(0, 0, 1);
        temp = newTrack.transform.localScale;
        temp.x = Vector2.Distance(from.transform.position, to.transform.position);
        newTrack.transform.localScale = temp;
        newTrack.name = from.name + "+" + to.name;
        newTrack.GetComponent<RailScript>().From = from; 
        newTrack.GetComponent<RailScript>().To = to;

    }


    //public void Generate()
    //{

    //    //Debug.Log("Editor causes this Update");
    //    foreach (Transform child in GameObject.Find("Rails").transform)//usuwanie starych torów na rzecz utworzenia nowych
    //    {
    //        if (Application.isPlaying == false)
    //            DestroyImmediate(child.gameObject);
    //        else
    //            Destroy(child.gameObject);
    //    }
    //    foreach (Waypoint child in UnityEngine.Object.FindObjectsOfType<Waypoint>())// pętla dla każdego waypointa na poziomie.
    //    {
    //        if (child.Ways.Count == 0)
    //        {
    //            Debug.LogError($"Waypoint {child.ObjectName} does not have any neighbours");
    //            continue;
    //        }
    //        if (!child.IsJunction) // nie jest zwrotnicą i jest uzupełniony
    //        {
    //            //tworzenie toru z A do B
    //            if (!child.TrackGenerated && child.Ways[0].Ways.Count != 3 && !(child.IsPlatform && child.Ways[0].IsPlatform))//jeśli tor nie został już wygenerowany oraz następca nie jest środkowym punktem zwrotnicy oraz ten punkt i punkt ways[0] nie tworzą peronu
    //            {
    //                //tworzenie toru
    //                //child.TrackGenerated = true;
    //                //child.ways[0].TrackGenerated = true;
    //                _katownik = child.Ways[0].transform.position - child.transform.position;
    //                Angle = Vector3.Angle(_wersorY, _katownik);

    //                var s1 = child.name + "+" + child.GetComponent<Waypoint>().Ways[0].name;
    //                var s2 = child.GetComponent<Waypoint>().Ways[0].name + "+" + child.name;

    //                if (GameObject.Find(s1) != null || GameObject.Find(s2) != null)
    //                {
    //                    return;
    //                }

    //                GameObject newTrack = Instantiate(Resources.Load("gamePrefabs/Rail/Rail")) as GameObject;
    //                newTrack.transform.SetParent(GameObject.Find("Rails").transform, false);
    //                if (child.Ways[0].transform.position.y < child.transform.position.y)
    //                {
    //                    newTrack.transform.Rotate(0, 0, -Angle);
    //                }
    //                else newTrack.transform.Rotate(0, 0, Angle);

    //                newTrack.transform.position = child.transform.position + (_katownik / 2) + new Vector3(0, 0, 1);
    //                temp = newTrack.transform.localScale;
    //                temp.x = Vector2.Distance(child.transform.position, child.GetComponent<Waypoint>().Ways[0].transform.position);
    //                newTrack.transform.localScale = temp;
    //                newTrack.name = child.name + "+" + child.GetComponent<Waypoint>().Ways[0].name;
    //                newTrack.GetComponent<RailScript>().From = child;
    //                newTrack.GetComponent<RailScript>().To = child.GetComponent<Waypoint>().Ways[0];



    //                //tworzenie toru z A do C
    //                if (child.GetComponent<Waypoint>().Ways.Count > 1 && !child.Ways[1].IsJunction && !(child.IsPlatform && child.Ways[1].IsPlatform))//jeśli punkt posiada drugą drogę oraz następca nie jest środkowym punktem zwrotnicy oraz ten punkt i punkt ways[1] nie tworzą peronu
    //                {
    //                    //child.GetComponent<waypoint>().ways[1].TrackGenerated = true;
    //                    _katownik = child.GetComponent<Waypoint>().Ways[1].transform.position - child.transform.position;
    //                    Angle = Vector3.Angle(_wersorY, _katownik);

    //                    newTrack = Instantiate(Resources.Load("gamePrefabs/Rail/Rail")) as GameObject;
    //                    newTrack.transform.SetParent(GameObject.Find("Rails").transform, false);
    //                    if (child.GetComponent<Waypoint>().Ways[1].transform.position.y < child.transform.position.y)
    //                    {
    //                        newTrack.transform.Rotate(0, 0, -Angle);
    //                    }
    //                    else newTrack.transform.Rotate(0, 0, Angle);

    //                    newTrack.transform.position = child.transform.position + (_katownik / 2) + new Vector3(0, 0, 1);
    //                    temp = newTrack.transform.localScale;
    //                    temp.x = Vector2.Distance(child.transform.position, child.GetComponent<Waypoint>().Ways[1].transform.position);
    //                    newTrack.transform.localScale = temp;
    //                    newTrack.name = child.name + "+" + child.GetComponent<Waypoint>().Ways[1].name;
    //                }
    //            }
    //        }
    //    }
    //}

    // Update is called once per frame


}

