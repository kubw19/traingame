using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadLvl : MonoBehaviour, IPointerDownHandler
{
    public string Level;
    public int MinimalStarsAmount;
    public LoadLvl [] Prev;
    public bool Locked = true;
    public void CanWeUnlock()
    {
        
    }
    //public Animator anim;
    //Color32 Faded = new Color32(0, 0, 0, 255);
    //public Image Fading;
   // public Image Fade;
    public void loadlevel()
    {
        SceneManager.LoadScene(Level);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // anim.Play("FadeOutLvlSelect");
        loadlevel();
    }
    void Start()
    {
       
    }
}
