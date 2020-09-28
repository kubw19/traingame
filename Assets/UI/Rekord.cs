using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rekord : MonoBehaviour {

    public TrainRecord Train;
    public Text id, from, to, arrival, ArrivalDelay, departure, DepartureDelay, platform, enrtyapproval, OrderNumber, Status;
    public Image Arrived, Departed;
    public float timer;
    int DeltaInMinutes;
    public static string ToFormatedClock(int clock, bool WithSecondsNoHours = false)
    {
        int seconds = (clock % 3600) % 60;
        int minutes = (clock % 3600)/60;
        int hours = clock / 3600;
        if (hours > 23) hours = 0;
        if (!WithSecondsNoHours)
        {
            if (minutes < 10)
            {
                if (hours < 10) return "0" + hours.ToString() + ":0" + minutes.ToString();
                else return hours.ToString() + ":0" + minutes.ToString();
            }
            else
            {
                if (hours < 10) return "0" + hours.ToString() + ":" + minutes.ToString();
                else return hours.ToString() + ":" + minutes.ToString();
            }
        }
        else
        {
            if (minutes < 10)
            {
                if (seconds < 10) return "0" + minutes.ToString() + ":0" + seconds.ToString();
                else return "0" + minutes.ToString() + ":" + seconds.ToString();
            }
            else
            {
                if (seconds < 10) return minutes.ToString() + ":0" + seconds.ToString();
                else return minutes.ToString() + ":" + seconds.ToString();
            }
        }
    }
    string DelayDisplay(int clock)
    {
        int minutes = clock/60;
        if (minutes < 10) return "+0" + minutes.ToString();
        else return "+" + minutes.ToString();
    }
    public int TrainColorInfo = 0; // 0-żółty  1-zielony   2-przezroczysty 3-czerwony
    bool playedDelay = false;
    void FillDelay()
    {
        if(Train.ArrDelay>60)ArrivalDelay.text = DelayDisplay(Train.ArrDelay);
        if(Train.DepDelay>60)DepartureDelay.text = DelayDisplay(Train.DepDelay);
    }
    
    void FillRecord()
    {
        id.text = Train.Id.ToString();
        from.text = Train.StartingPoint;
        to.text = Train.EndingPoint;
        arrival.text = ToFormatedClock(Train.ArrivalTime);
        departure.text = ToFormatedClock(Train.DepartureTime);
        platform.text = Train.Peron.ToString();
        Status.text = Train.TrainUnit.Status;
        FillDelay();
    }

    void ColorRecord()
    {
        if (Train.ArrivalTime - TrainGame.Generator().clock < 180)
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 164, 4, 220);

            if (Train.TrainUnit.Go == true)
            {
                gameObject.GetComponent<Image>().color = new Color32(62, 219, 28, 220);
                TrainColorInfo = 1;
            }
            if (Train.ArrDelay > 300 || Train.DepDelay > 300)
            {
                gameObject.GetComponent<Image>().color = new Color32(245, 17, 17, 220);
                TrainColorInfo = 3;
                if (!playedDelay)
                {
                    GameObject.Find("DelayedSound").GetComponent<AudioSource>().Play();
                    playedDelay = true;
                }
            }

            DeltaInMinutes = (Train.DepartureTime / 60 - TrainGame.Generator().clock/60);
             if (DeltaInMinutes <= 1 && DeltaInMinutes>= -5 && Train.TrainUnit.Go && Train.TrainUnit.Arrived && !Train.TrainUnit.Departed) 
                {
                timer += Time.deltaTime; //TrainGame.Generator().clock;
                if (timer<1)
                {
                    gameObject.GetComponent<Image>().color = new Color32(62, 219, 28, 0);
                    TrainColorInfo = 2;
                }
                else
                {
                    gameObject.GetComponent<Image>().color = new Color32(62, 219, 28, 220);
                    TrainColorInfo = 1;
                }
                if (timer >= 2) timer = 0;
            }
        }
    }

    void CheckState()
    {
        if (Train.TrainUnit.Arrived) Arrived.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Ticks/tick");
        else Arrived.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Ticks/emptytick");
        if (Train.TrainUnit.Departed) Departed.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Ticks/tick");
        else Departed.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Ticks/emptytick");
    }
    private void FixedUpdate()
    {
        FillRecord();
        ColorRecord();
        CheckState();
    }
}
