using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainGame : MonoBehaviour {

    public static Generator Generator()
    {
        return GameObject.Find("Canvas").GetComponent<Generator>();

    }
}
