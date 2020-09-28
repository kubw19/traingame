using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Train : MonoBehaviour {

    //Funkcja zwracająca kurs poruszającego się pociągu
    float getHeading()
    {
        Vector3 wektor1, wektor2;
        float Heading;
        wektor1 = next.transform.position - lastVisited.transform.position;
        wektor2 = new Vector3(0, 5, 0);
        Heading = Vector3.SignedAngle(wektor1, wektor2, Vector3.forward);
        if (Heading < 0) Heading = 360 + Heading;
        return Heading;
    }
}
