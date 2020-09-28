using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarsAmount : MonoBehaviour {

    public bool slider = false;

    // Use this for initialization
    float Stars;
    void Start() {
        for (int i = -1; i > -2; i--)
        {
            Stars += PlayerPrefs.GetInt(i.ToString(), 0);
        }
        if (!slider)
        {
            this.GetComponent<Text>().text = "x" + Stars.ToString();
        }
	}

    private void Update()
    {
        if (slider)
        {
            float minimalStarsAmount = GameObject.Find("Canvas").GetComponent<LvlUnlocks>().Poziomy[1].MinimalStarsAmount;

            if(Stars / minimalStarsAmount > 1)
                this.GetComponent<Slider>().value = 1;
            else
            {
                this.GetComponent<Slider>().value = Stars / minimalStarsAmount;
            }
            this.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = Stars.ToString() + "/" + minimalStarsAmount.ToString();
        }
    }

}
