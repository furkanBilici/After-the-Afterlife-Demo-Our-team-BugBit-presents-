using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WaitCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Front;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Front.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Front.SetActive(true);
    }

   
}
