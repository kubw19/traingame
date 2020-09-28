using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    private Material temp;
    public waypoint toToggle;
   // Color32 On = new Color32();
   // Color32 Off = new Color32();
    Color32 Temp;
    Color32 Temp2;
    private void OnMouseDown()
    {
        if (toToggle.train == null /*&& GetComponentInParent<JunctionFail>().Damaged == false*/)
        {
            toToggle.Toggle(toToggle);
            GameObject straight = this.transform.parent.transform.Find("straight").gameObject;
            GameObject side = this.transform.parent.transform.Find("side").gameObject;
            Temp = side.GetComponent<Renderer>().material.color;
            Temp2 = straight.GetComponent<Renderer>().material.color;
            straight.GetComponent<MeshRenderer>().material.color = Temp;
            side.GetComponent<Renderer>().material.color = Temp2;



        }
        else if (toToggle.train != null/* && GetComponentInParent<JunctionFail>().Damaged == false*/)
        {
            TrainGame.Generator().GameOver(2);
            Debug.Log("GameOver - You tried to switch junction with a train on it!");
        }
    }   
}
