using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class LvlId : MonoBehaviour, IPointerDownHandler {

    public int Id;
    LvlUnlocks Canvas;
    bool FirstColour = false;
    public string levelName;
    public int Hearts;
    public GameObject unlockedBlock;
    public int minimalStarsAmount;
    public bool padlockOpen = false;
    bool PrevsChecking(LevelsUnlocks Lvl) //sprawdzenie, czy odblokowane są poprzednie poziomy
    {
        if (Lvl.Prev[0] != null)
        {
            foreach (LevelsUnlocks Level in Lvl.Prev)
            {
                if (Level.Locked == true) return false;
            }
        }
        return true;
    }

    void poziomOdblokowany()
    {
        transform.Find("padlock").gameObject.SetActive(false);
        unlockedBlock.SetActive(true);
        Color c = unlockedBlock.transform.Find("levelNameText").GetComponent<TextMeshProUGUI>().color;
        c.a = 1;
        unlockedBlock.transform.Find("levelNameText").GetComponent<TextMeshProUGUI>().color = c;
        unlockedBlock.transform.Find("levelNameText").GetComponent<TextMeshProUGUI>().text = levelName;
    }

    void odblokowaniePoziomu()
    {
        this.transform.Find("padlock").GetComponent<Animator>().Play("otworz");
        GameObject.Find("LvlUnlocker").GetComponent<AudioSource>().Play();
    }

    public void tekstOdblokowania()
    {
        unlockedBlock.SetActive(true);
        unlockedBlock.transform.Find("levelNameText").GetComponent<TextMeshProUGUI>().text = levelName;
    }

    void grafikaZablokowania()
    {
        //this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/locked");
       // lockedBlock.transform.Find("starsAmount").GetComponent<TextMeshProUGUI>().text = minimalStarsAmount.ToString();
    }
    public void OnPointerDown(PointerEventData data)
    {
        //Debug.Log("klil");
        if (Canvas.Poziomy[Id].MinimalStarsAmount <= Canvas.Stars && PrevsChecking(Canvas.Poziomy[Id]) && Canvas.Poziomy[Id].Locked)
        {
            //odblokowanie poziomu jeśli jest odpowiednia liczba gwazd i poprzednie są odblokowane
            Canvas.Poziomy[Id].Locked = false;
            odblokowaniePoziomu();
            Canvas.IncreaseMinimumStars();
            Canvas.UpdateLevelData();
        }
        else if (!Canvas.Poziomy[Id].Locked && Hearts > 0)
        {
            //wejście w poziom jeśli poziom jest odblokowany i są jakieś życia do wykorzystania
            Hearts = Hearts - 1;
            PlayerPrefs.SetInt("Hearts", Hearts);
            GameObject.Find("HeartsCounterController").GetComponent<NewLiveTimer>().SaveHealTime();
            Destroy(GameObject.Find("HeartsCounterController"));
            GameObject.Find("LevelPasser").GetComponent<LevelPasser>().SceneName = Canvas.Poziomy[Id].Name;
            SceneManager.LoadScene("LoadingLevel");
        }

    }
    void Start()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<LvlUnlocks>();
        if (Canvas.Poziomy[Id].Locked == false)
        {
            poziomOdblokowany();
        }



        
    }
    void FixedUpdate()
    {
        Hearts = PlayerPrefs.GetInt("Hearts",5);
        if(Canvas.DataLoaded && !FirstColour)
        {
            
            if (Canvas.Poziomy[Id].Locked)
            {

                grafikaZablokowania();
            }
            FirstColour = true;
        }
    }
}
