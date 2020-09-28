using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonClickSound : MonoBehaviour, IPointerDownHandler
{
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.Find("ButtonClickSound").GetComponent<AudioSource>();
        DontDestroyOnLoad(GameObject.Find("Audio"));
    }

    public void OnPointerDown(PointerEventData data)
    {
        sound.Play();
    }
}
