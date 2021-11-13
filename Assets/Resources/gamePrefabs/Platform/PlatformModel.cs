using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformModel : MonoBehaviour {

    void OnMouseDown()
    {
        this.transform.parent.transform.parent.GetComponent<GenHandle>().Generator.ChoosenPlatform = this.GetComponentInParent<PlatformGroup>();
        TrainGame.Generator().ChoosenPlatText.text = this.GetComponentInParent<PlatformGroup>().PlatformId.ToString();
    }
}
