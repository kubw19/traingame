using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    void OnMouseDown()
    {
        this.transform.parent.transform.parent.GetComponent<GenHandle>().Generator.ChoosenPlatform = this.GetComponentInParent<PeronFolder>();
        TrainGame.Generator().ChoosenPlatText.text = this.GetComponentInParent<PeronFolder>().PlatformId.ToString();
    }
}
