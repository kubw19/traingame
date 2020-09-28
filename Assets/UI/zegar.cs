using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zegar : MonoBehaviour
{

    int minutes, hours,seconds;
    public int clock;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        minutes = (clock % 3600)/60;
        hours = clock / 3600;
        seconds = clock - 3600 * hours - 60 * minutes;
        if (hours > 23) hours = 0;

        if (minutes < 10)
        {
            if (hours < 10 && seconds < 10) this.GetComponent<Text>().text = "0" + hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
            else if (hours<10 && seconds >10) this.GetComponent<Text>().text = "0"+ hours.ToString() + ":0" + minutes.ToString() + ":" + seconds.ToString();
            else if (hours > 10 && seconds <10) this.GetComponent<Text>().text = hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
            else if (hours > 10 && seconds > 10) this.GetComponent<Text>().text = hours.ToString() + ":0" + minutes.ToString() + ":" + seconds.ToString();
        }
        else
        {
            if (hours < 10 && seconds < 10) this.GetComponent<Text>().text = "0" + hours.ToString() + ":" + minutes.ToString() + ":0" + seconds.ToString();
            else if (hours < 10 && seconds > 10) this.GetComponent<Text>().text = "0" + hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
            else if (hours > 10 && seconds < 10) this.GetComponent<Text>().text = hours.ToString() + ":" + minutes.ToString() + ":0" + seconds.ToString();
            else if (hours > 10 && seconds > 10) this.GetComponent<Text>().text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
        }
    }
}
