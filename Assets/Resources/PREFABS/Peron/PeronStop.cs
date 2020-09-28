using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeronStop : MonoBehaviour {

    [HideInInspector]  public Texture[] Textures;
    private Texture temp;
    [HideInInspector] public Peron Point;

    public void SetRed()
    {
        GetComponent<Renderer>().material.mainTexture = this.transform.parent.transform.parent.transform.parent.GetComponent<_Semafory>().Textures[0]; ;
    }

    public void SetGreen()
    {
        GetComponent<Renderer>().material.mainTexture = this.transform.parent.transform.parent.transform.parent.GetComponent<_Semafory>().Textures[1]; ;
    }


    public void Toggle()
    {
        Point = this.transform.parent.GetComponent<Peron>();
        Point.Wait = !Point.Wait;
        if (Point.Wait) SetRed();
        else SetGreen();

    }

    private void OnMouseDown()
    {
        Toggle();
    }
}
