using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoneButton : MonoBehaviour
{

    Generator Gen;


    private void Start()
    {
        Gen = TrainGame.Generator();

    }
    public void Close()
    {
        if (Gen.ChoosenPlatform != null && Gen.ChangingPlatTrain != null)
        {
            Gen.ChangingPlatTrain.Peron = Gen.ChoosenPlatform.PlatformId;
        }

        this.gameObject.SetActive(false);
        Gen.ChoosenPlatform = null;
        Gen.ChangingPlatTrain = null;
        Gen.PreventFromZoom = false;

    }
}
