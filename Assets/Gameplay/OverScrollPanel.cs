using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverScrollPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        TrainGame.Generator().PreventFromZoom = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TrainGame.Generator().PreventFromZoom = false;
    }

}
