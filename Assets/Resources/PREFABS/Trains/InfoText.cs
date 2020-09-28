using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour {

    private void Start()
    {
        this.GetComponent<Text>().text = this.transform.parent.GetComponentInParent<Train>().TrainRecord.Id.ToString();
    }
}
