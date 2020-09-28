using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionFail : MonoBehaviour
{
    System.Random rand = new System.Random();
    public bool Damageable;
    public bool Damaged;
    Generator gen;
    float timer;

    void Start()
    {
        gen = TrainGame.Generator();
    }


    void FixedUpdate()
    {
        if (Damageable && !gen.Emergency && !Damaged)
        {
            int los = rand.Next(0, gen.probability);
            if (los == 1) //prawdopodobieństwo
            {
                Damaged = true;
                gen.Emergency = true;
                Debug.Log("Awaria zwrotnicy");
            }
        }
        if(Damaged)
        {
            timer += Time.deltaTime * gen.CompressionRate;
            if (timer>=600f)
            {
                Debug.Log("Zwrotnica naprawiona");
                Damaged = false;
                gen.Emergency = false;
            }
        }

    }
}