using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RecordButton : MonoBehaviour {

    public Rekord Rekord;
    public Button Button;
    bool Approved = false;

    public void ApproveEntry()
    {
        Rekord.Train.TrainUnit.TrainEntryApproval();
        Button.GetComponentInChildren<Text>().text = "Approved";
        Approved = true;
        Button.interactable = false;
        TrainGame.Generator().PreventFromZoom = false;

    }

    public void ChoosePlatform()
    {


        this.transform.parent.transform.parent.GetComponent<CPBGHandle>().CPBG.gameObject.SetActive(true);
        TrainGame.Generator().ChangingPlatTrain = this.transform.parent.GetComponent<Rekord>().Train;

        TrainGame.Generator().ChoosenPlatText.text = TrainGame.Generator().ChangingPlatTrain.Peron.ToString();
    }

    private void FixedUpdate()
    {
        if (Rekord.Train.ArrivalTime - Rekord.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Generator>().clock > 180) Button.interactable = false;
        else if (!Approved)
        {
            Button.interactable = true;

        }
        
    }
}
