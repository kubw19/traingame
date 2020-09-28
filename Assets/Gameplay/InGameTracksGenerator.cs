using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InGameTracksGenerator : MonoBehaviour
{

    Vector3 WersorY = new Vector3(1, 0, 0);
    Vector3 Katownik;
    public Vector3 temp;
    public float Angle;
    // Use this for initialization
    private void Awake()//Ustawianie domyślnych wartości wygenerowania toru dla punktu na false - na razie nieużywane
    {
        foreach (waypoint child in Object.FindObjectsOfType<waypoint>())
        {
            child.TrackGenerated = false;

        }
    }
    void Start()
    {
        foreach (Transform child in GameObject.Find("Tory").transform)//usuwanie starych torów na rzecz utworzenia nowych
        {
            if (Application.isPlaying==false)
               DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
        }
        foreach (waypoint child in Object.FindObjectsOfType<waypoint>())// pętla dla każdego waypointa na poziomie.
        {
            if (child.ways[2] == null)
            {
                //tworzenie toru z A do B
                if (!child.TrackGenerated && child.ways[0].ways[2] == null && !(child.peron && child.ways[0].peron))//jeśli tor nie został już wygenerowany oraz następca nie jest środkowym punktem zwrotnicy oraz ten punkt i punkt ways[0] nie tworzą peronu
                {
                    //tworzenie toru
                    //child.TrackGenerated = true;
                    //child.ways[0].TrackGenerated = true;
                    Katownik = child.ways[0].transform.position - child.transform.position;
                    Angle = Vector3.Angle(WersorY, Katownik);

                    GameObject newTrack = Instantiate(Resources.Load("PREFABS/Tor/TorTemplate")) as GameObject;
                    newTrack.transform.SetParent(GameObject.Find("Tory").transform, false);
                    if (child.ways[0].transform.position.y < child.transform.position.y)
                    {
                        newTrack.transform.Rotate(0, 0, -Angle);
                    }
                    else newTrack.transform.Rotate(0, 0, Angle);

                    newTrack.transform.position = child.transform.position + (Katownik / 2) + new Vector3(0, 0, 1);
                    temp = newTrack.transform.localScale;
                    temp.x = Vector2.Distance(child.transform.position, child.GetComponent<waypoint>().ways[0].transform.position);
                    newTrack.transform.localScale = temp;
                    newTrack.name = child.name + "+" + child.GetComponent<waypoint>().ways[0].name;


                    //tworzenie toru z A do C
                    if (child.GetComponent<waypoint>().ways[1] != null && child.ways[1].ways[2] == null && !(child.peron && child.ways[1].peron))//jeśli punkt posiada drugą drogę oraz następca nie jest środkowym punktem zwrotnicy oraz ten punkt i punkt ways[1] nie tworzą peronu
                    {
                        //child.GetComponent<waypoint>().ways[1].TrackGenerated = true;
                        Katownik = child.GetComponent<waypoint>().ways[1].transform.position - child.transform.position;
                        Angle = Vector3.Angle(WersorY, Katownik);

                        newTrack = Instantiate(Resources.Load("PREFABS/Tor/TorTemplate")) as GameObject;
                        newTrack.transform.SetParent(GameObject.Find("Tory").transform, false);
                        if (child.GetComponent<waypoint>().ways[1].transform.position.y < child.transform.position.y)
                        {
                            newTrack.transform.Rotate(0, 0, -Angle);
                        }
                        else newTrack.transform.Rotate(0, 0, Angle);

                        newTrack.transform.position = child.transform.position + (Katownik / 2) + new Vector3(0, 0, 1);
                        temp = newTrack.transform.localScale;
                        temp.x = Vector2.Distance(child.transform.position, child.GetComponent<waypoint>().ways[1].transform.position);
                        newTrack.transform.localScale = temp;
                        newTrack.name = child.name + "+" + child.GetComponent<waypoint>().ways[1].name;
                    }
                }
            }
        }
    }

    // Update is called once per frame


}

