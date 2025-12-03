using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpriteButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }
}
