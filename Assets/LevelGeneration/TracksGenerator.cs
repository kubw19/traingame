
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
    void Update()
    {
       
        //Debug.Log("Editor causes this Update");
        foreach (Transform child in GameObject.Find("Rails").transform)//usuwanie starych torów na rzecz utworzenia nowych
        {
            if (Application.isPlaying==false)
               DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
        }
        foreach (Waypoint child in UnityEngine.Object.FindObjectsOfType<Waypoint>())// pętla dla każdego waypointa na poziomie.
        {
            if (child.ways[0] != null && child.ways.Length != 3) // nie jest zwrotnicą i jest uzupełniony
            {
                //tworzenie toru z A do B
                if (!child.TrackGenerated && child.ways[0].ways.Length != 3 && !(child.IsPlatform && child.ways[0].IsPlatform))//jeśli tor nie został już wygenerowany oraz następca nie jest środkowym punktem zwrotnicy oraz ten punkt i punkt ways[0] nie tworzą peronu
                {
                    //tworzenie toru
                    //child.TrackGenerated = true;
                    //child.ways[0].TrackGenerated = true;
                    _katownik = child.ways[0].transform.position - child.transform.position;
                    Angle = Vector3.Angle(_wersorY, _katownik);

                    GameObject newTrack = Instantiate(Resources.Load("gamePrefabs/Rail/Rail")) as GameObject;
                    newTrack.transform.SetParent(GameObject.Find("Rails").transform, false);
                    if (child.ways[0].transform.position.y < child.transform.position.y)
                    {
                        newTrack.transform.Rotate(0, 0, -Angle);
                    }
                    else newTrack.transform.Rotate(0, 0, Angle);

                    newTrack.transform.position = child.transform.position + (_katownik / 2) + new Vector3(0, 0, 1);
                    temp = newTrack.transform.localScale;
                    temp.x = Vector2.Distance(child.transform.position, child.GetComponent<Waypoint>().ways[0].transform.position);
                    newTrack.transform.localScale = temp;
                    newTrack.name = child.name + "+" + child.GetComponent<Waypoint>().ways[0].name;


                    //tworzenie toru z A do C
                    if (child.GetComponent<Waypoint>().ways.Length > 1 && child.ways[1].ways.Length != 3 && !(child.IsPlatform && child.ways[1].IsPlatform))//jeśli punkt posiada drugą drogę oraz następca nie jest środkowym punktem zwrotnicy oraz ten punkt i punkt ways[1] nie tworzą peronu
                    {
                        //child.GetComponent<waypoint>().ways[1].TrackGenerated = true;
                        _katownik = child.GetComponent<Waypoint>().ways[1].transform.position - child.transform.position;
                        Angle = Vector3.Angle(_wersorY, _katownik);

                        newTrack = Instantiate(Resources.Load("gamePrefabs/Rail/Rail")) as GameObject;
                        newTrack.transform.SetParent(GameObject.Find("Rails").transform, false);
                        if (child.GetComponent<Waypoint>().ways[1].transform.position.y < child.transform.position.y)
                        {
                            newTrack.transform.Rotate(0, 0, -Angle);
                        }
                        else newTrack.transform.Rotate(0, 0, Angle);

                        newTrack.transform.position = child.transform.position + (_katownik / 2) + new Vector3(0, 0, 1);
                        temp = newTrack.transform.localScale;
                        temp.x = Vector2.Distance(child.transform.position, child.GetComponent<Waypoint>().ways[1].transform.position);
                        newTrack.transform.localScale = temp;
                        newTrack.name = child.name + "+" + child.GetComponent<Waypoint>().ways[1].name;
                    }
                }
            }
        }
    }

    // Update is called once per frame


}

