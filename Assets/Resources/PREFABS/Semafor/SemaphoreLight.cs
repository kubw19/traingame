using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemaphoreLight : MonoBehaviour {
    /*
     klasa światła semafora; jako argumenty przyjmuje tekstury światła w postaci tablicy. 
     Pierwszym jej elementem jest tekstura światła czerwonego, drugim zielonego.

     Toggle zmienia tekstury swiatła oraz zmienia wartośc logiczną zmiennej stop powiązanego punktu
     Toggle wywołuje się po naciśnięciu na teksturę
    */
    [HideInInspector] public Texture[] Textures;
    private Semaphore Point;

    private Texture temp;

    public void SetRed()
    {
        var c = GetComponent<Renderer>();
        c.material = null;
        GetComponent<Renderer>().material.mainTexture = GameObject.Find("Semaphores").GetComponent<_Semafory>().Textures[0];
    }

    public void SetGreen()
    {
        GetComponent<Renderer>().material.mainTexture = GameObject.Find("Semaphores").GetComponent<_Semafory>().Textures[1];
    }


    public void Toggle()
    {
        Point = this.transform.parent.GetComponent<Semaphore>();
        Point.Stop = !Point.Stop;
        if (Point.Stop) SetRed();
        else SetGreen();
    }

    private void OnMouseDown()
    {
        Toggle();
    }
}
