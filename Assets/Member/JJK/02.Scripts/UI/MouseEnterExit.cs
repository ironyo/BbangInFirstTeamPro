using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action OnMouseEnter;
    public Action OnMouseExit;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter?.Invoke();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit?.Invoke();
    }
}
