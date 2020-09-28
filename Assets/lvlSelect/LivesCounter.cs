using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LivesCounter : MonoBehaviour {
    
   
	void Update ()
    {
        if (GameObject.Find("HeartsCounterController").GetComponent<NewLiveTimer>().Lives != 5)
        {
            System.TimeSpan Difference = (GameObject.Find("HeartsCounterController").GetComponent<NewLiveTimer>().FirstHealTime().time - System.DateTime.Now);
            double SecondsTime = Difference.TotalSeconds;
            int Seconds = (int)SecondsTime;
            GetComponent<Text>().text = Rekord.ToFormatedClock(Seconds, true);
        }
        else GetComponent<Text>().text = "";
            
        
	}
}
