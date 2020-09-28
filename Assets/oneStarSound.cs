using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class oneStarSound : MonoBehaviour, IPointerDownHandler
{
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.Find("1 Star").GetComponent<AudioSource>();
        DontDestroyOnLoad(GameObject.Find("Audio"));
    }

    public void OnPointerDown(PointerEventData data)
    {
        sound.Play();
    }
}