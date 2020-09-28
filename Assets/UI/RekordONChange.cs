using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RekordONChange : MonoBehaviour {

    public static void ResetNumber()
    {
        foreach (Transform Child in GameObject.Find("Rekordy").transform)
        {
            TrainGame.Generator().TrainAmount++;
            Child.GetComponent<Rekord>().OrderNumber.text = TrainGame.Generator().TrainAmount.ToString();
        }
        TrainGame.Generator().TrainAmount = -1;
    }
}
