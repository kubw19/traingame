using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CompressionChange : MonoBehaviour, IPointerDownHandler
{
    Generator gener;
    public bool plus;
    float CompTemp;

    void Start()
    {
        gener = TrainGame.Generator();
    }
    void VelocityChange(float tempCompression)
    {
        foreach (TrainRecord trainrecord in gener.ScheduledTrains)
        {
            trainrecord.TrainUnit.velocity = trainrecord.TrainUnit.velocity;// * tempCompression;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
      if (plus)
        {
            CompTemp = gener.CompressionRate;
            if (gener.CompressionRate == 1) gener.CompressionRate+=4;
            else if (gener.CompressionRate == 5) gener.CompressionRate += 5;
            else if (gener.CompressionRate == 10) gener.CompressionRate += 20;
            else if (gener.CompressionRate == 30) gener.CompressionRate += 30;
            else if (gener.CompressionRate == 60) gener.CompressionRate += 0;
           
            VelocityChange(gener.CompressionRate/CompTemp);
        }
      else
        {
            CompTemp = gener.CompressionRate;
            if (gener.CompressionRate == 1) gener.CompressionRate -= 0;
            else if (gener.CompressionRate == 5) gener.CompressionRate -= 4;
            else if (gener.CompressionRate == 10) gener.CompressionRate -= 5;
            else if (gener.CompressionRate == 30) gener.CompressionRate -= 20;
            else if (gener.CompressionRate == 60) gener.CompressionRate -= 30;
            VelocityChange(gener.CompressionRate/CompTemp);
        }
    }

}

