using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour {
    float lerpDuration = 0.5f;
    float lerpStart = 1.0f;
    bool start = false;
    public GameObject unlockText;
public void fadeOut()
    {
        start = true;
        lerpStart = Time.time;
    }
    private void Update()
    {
        if (start)
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<MeshRenderer>() != null)
                {
                    var progress = Time.time - lerpStart;
                    Color c = child.GetComponent<MeshRenderer>().material.color;
                    if (c.a == 0) start = false;

                    c.a = Mathf.Lerp(1.0f, 0.0f, progress / lerpDuration);

                    child.GetComponent<MeshRenderer>().material.color =  c;
                    Debug.Log(c.a);

                    if (c.a == 0)
                    {
                        this.transform.parent.GetComponent<LvlId>().tekstOdblokowania();

                        Destroy(gameObject);
                    }
                }

                
            }
        }
    }
}


