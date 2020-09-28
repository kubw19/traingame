using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelectTrainGenerator : MonoBehaviour {
    LvlUnlocks sterownik;
    float newTrainsFreq = 1.5f;//częstotliwośc tworzenia się nowych pociągów
    float timer = -1;//timer tworzenia pociągów

    bool trainMakingEnabled = false;//przełącznik tworzenia nowych pociągów

    public int licznik = 0;//licznik nazw pociągów

    public void loadLevelSelectTrains () {
        trainMakingEnabled = true;
	}

    void makeTrains()
    {
        sterownik = this.GetComponent<LvlUnlocks>();

        foreach (LevelsUnlocks poziom in sterownik.Poziomy)//tworzenie pociągów dla każdej ścieżki 
        {
            if (poziom.availableDirections != null)
            {
                foreach (blockDirection dir in poziom.availableDirections)
                {

                    stickPath newPath = dir.paths.Dequeue();
                   // if (!newPath.to.Locked)
                    {
                        GameObject newTrain = Instantiate(Resources.Load("PREFABS/lvlSelectTrain"), GameObject.Find("Bloki").transform) as GameObject;
                        newTrain.name = licznik.ToString() + "pociag";
                        licznik++;//licznik nazwy pociągów

                        newTrain.GetComponent<levelSelectTrain>().path = newPath;//przypisanie ścieżki pociągowi
                        newTrain.GetComponent<levelSelectTrain>().move = true;
                    }
                    dir.paths.Enqueue(newPath);

                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (trainMakingEnabled)//tworzenie nowych pociągów
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timer = newTrainsFreq;
                makeTrains();
            }
        }

    }
}
