using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrains : MonoBehaviour {

    void NewTrain(){
        GameObject newTrain = Instantiate(Resources.Load("PREFABS/Trains/menutraintemplate"), GameObject.Find("Trains").transform, true) as GameObject;
    }	

	void Start () {

        InvokeRepeating("NewTrain", 0f, 0.4f);

    }
}
