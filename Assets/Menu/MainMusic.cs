using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMusic : MonoBehaviour {

    bool Muted = false;
    int MuteInt = 0;

    void Mute()
    {
        GameObject.Find("AudioManager").GetComponent<AudioSource>().mute = true;
        Muted = true;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/mute");
        PlayerPrefs.SetInt("Muted", 1);
    }

    void UnMute()
    {
        GameObject.Find("AudioManager").GetComponent<AudioSource>().mute = false;
        Muted = false;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/unmute");
        PlayerPrefs.SetInt("Muted", 0);
    }

    private void Start()
    {
        DontDestroyOnLoad(GameObject.Find("AudioManager"));
        MuteInt = PlayerPrefs.GetInt("Muted", 0);
        if (MuteInt == 0) UnMute();
        else Mute();
    }

    public void Click()
    {
        if (!Muted)
        {
            Mute();
        }
        else
        {
            UnMute();
        }
       
    }

}
