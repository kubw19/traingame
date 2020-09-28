using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Text>().text = "Platform " + this.transform.parent.transform.parent.transform.parent.GetComponent<PeronFolder>().PlatformId.ToString();		
	}
	
}
