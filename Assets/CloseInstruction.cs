using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseInstruction : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject title;
    PresentInstruction instructionHandler;
    void Awake()
    {
        instructionHandler = GameObject.Find("Instruction").GetComponent<PresentInstruction>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Invoke("Disactivate", 1f);
        Disactivate2();
        instructionHandler.anime.Play("HidePanel");
        
    }
   void Disactivate()
        {
            instructionHandler.activeCanvas = instructionHandler.ChangeActive(instructionHandler.activeCanvas);
            instructionHandler.instructionCanvas.SetActive(instructionHandler.activeCanvas);
        }
    void Disactivate2()
    {
        title.SetActive(false);
    }
}
