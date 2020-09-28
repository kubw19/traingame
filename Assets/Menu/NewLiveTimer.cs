using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class NewLiveTimer : MonoBehaviour {
    public System.DateTime TimeMinus;
    public System.DateTime TimeNewLife;
    bool TimeKnown = false;
    HealTime Pointer;
    public List<HealTime> HealTimeStamps;
    public int Lives;
    bool TimeSet = false;

    HealTime Soonest = new HealTime();

    public void SaveHealTime()
    {
       
        BinaryFormatter bf = new BinaryFormatter();
        FileStream LevelsData;

        if (!File.Exists(Application.persistentDataPath + "/HealTime.jmw"))
        {
            Pointer = new HealTime();
            Pointer.time = System.DateTime.Now;
            Pointer.time = Pointer.time.AddSeconds(30);
            HealTimeStamps.Add(Pointer);
            
            LevelsData = File.Open(Application.persistentDataPath + "/HealTime.jmw", FileMode.Create);
            bf.Serialize(LevelsData, HealTimeStamps);
        }
        else
        {
            LevelsData = File.Open(Application.persistentDataPath + "/HealTime.jmw", FileMode.Open);
            HealTimeStamps = (List<HealTime>)bf.Deserialize(LevelsData);
            LevelsData.Close();

            Pointer = new HealTime();

            if (HealTimeStamps.Count != 0)
            {
                Pointer.time = HealTimeStamps[HealTimeStamps.Count - 1].time;
                Pointer.time = Pointer.time.AddSeconds(30);
               
            }
            else
            {
                Pointer.time = System.DateTime.Now;
                Pointer.time = Pointer.time.AddSeconds(30);
               
            }
            HealTimeStamps.Add(Pointer);

            LevelsData = File.Open(Application.persistentDataPath + "/HealTime.jmw", FileMode.Create);
            bf.Serialize(LevelsData, HealTimeStamps);

        }


        LevelsData.Close();
    }

    public HealTime FirstHealTime(){//czas najbliższego odrodzenia życia
        //if (HealTimeStamps.Count == 0) return null;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream LevelsData = File.Open(Application.persistentDataPath + "/HealTime.jmw", FileMode.Open);
        HealTimeStamps = (List<HealTime>)bf.Deserialize(LevelsData);
        LevelsData.Close();
        return HealTimeStamps[0];
    }

    public void DeleteTimeStamp()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream LevelsData;
        LevelsData = File.Open(Application.persistentDataPath + "/HealTime.jmw", FileMode.Open);
        HealTimeStamps = (List<HealTime>)bf.Deserialize(LevelsData);
        LevelsData.Close();
        HealTimeStamps.RemoveAt(0);
        LevelsData = File.Open(Application.persistentDataPath + "/HealTime.jmw", FileMode.Create);
        bf.Serialize(LevelsData, HealTimeStamps);
        LevelsData.Close();
    }

   

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(this);
        Lives = PlayerPrefs.GetInt("Hearts", 5);
    }
	
	
	void Update ()
    {
        if (Lives < 5 && !TimeSet)
        {
            Soonest = FirstHealTime();
            //Debug.Log(Soonest.time);
            TimeSet = true;
        }
        else if (Lives < 5 && TimeSet)
        {
            if (Soonest.time <= System.DateTime.Now)
            {
                
                Lives++;
                PlayerPrefs.SetInt("Hearts", Lives);
                DeleteTimeStamp();
                TimeSet = false;
            }
        }
    }
}

[Serializable]
public class HealTime
{
    public System.DateTime time;
}