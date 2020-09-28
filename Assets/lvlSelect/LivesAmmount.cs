using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesAmmount : MonoBehaviour {

    public int Lives;
	void Start ()
    {
        //PlayerPrefs.DeleteAll();
        //Lives = PlayerPrefs.GetInt("Hearts", 5);
        //this.GetComponent<Text>().text = "x" + Lives.ToString();
        //AllLoaded = true;
    }

    void Update()
    {
        Lives = GameObject.Find("HeartsCounterController").GetComponent<NewLiveTimer>().Lives;
        this.GetComponent<Text>().text = "x" + Lives.ToString();
    }
	
}
