using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadLvlSelect : MonoBehaviour, IPointerDownHandler
{

    public void loadNextMenu(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        loadNextMenu("LvlSelect");
    }

}
	
	