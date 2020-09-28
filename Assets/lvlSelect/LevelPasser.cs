using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPasser : MonoBehaviour {


    public string SceneName;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

}
