using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonClickSound : MonoBehaviour, IPointerDownHandler
{
    AudioSource _sound;
    // Start is called before the first frame update
    void Start()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        _sound = GameObject.Find("ButtonClickSound")?.GetComponent<AudioSource>();
        var audio = GameObject.Find("Audio");
        if (audio != null)
        {
            DontDestroyOnLoad(audio);
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (_sound is null)
            return;
        _sound.Play();
    }
}
