using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PresentInstruction : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public bool activeCanvas = false;
    public GameObject instructionCanvas;
    [SerializeField] GameObject title;
    public Animator anime;
    private void Start()
    {
        instructionCanvas = GameObject.Find("InstructionPanel");
        instructionCanvas.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Invoke("Activate", 1f);
        activeCanvas = ChangeActive(activeCanvas);
        instructionCanvas.SetActive(activeCanvas);
        anime.Play("InstructionAnimator");
    }

    public bool ChangeActive (bool setValue)
    {
        if (setValue) setValue = false;
        else setValue = true;
        return setValue;
    }
    void Activate()
    {
        title.SetActive(true);
    }
}
