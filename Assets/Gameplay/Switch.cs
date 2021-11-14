using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    private Material _tempMaterial;
    public JunctionWaypoint toToggle;
   // Color32 On = new Color32();
   // Color32 Off = new Color32();

    private void OnMouseDown()
    {
        if (toToggle.train == null /*&& GetComponentInParent<JunctionFail>().Damaged == false*/)
        {
            toToggle.Toggle();
            GameObject straight = this.transform.parent.transform.Find("StraightRail").gameObject;
            GameObject side = this.transform.parent.transform.Find("SideRail").gameObject;
            var _temp = side.GetComponent<Renderer>().material;
            var _temp2 = straight.GetComponent<Renderer>().material;
            straight.GetComponent<MeshRenderer>().material = _temp;
            side.GetComponent<Renderer>().material = _temp2;



        }
        else if (toToggle.train != null/* && GetComponentInParent<JunctionFail>().Damaged == false*/)
        {
            TrainGame.Generator().GameOver(2);
            Debug.Log("GameOver - You tried to switch junction with a train on it!");
        }
    }   
}
