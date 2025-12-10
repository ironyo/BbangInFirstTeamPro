using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent onClick = new UnityEvent(); // Button이 쓰는 방식

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick.Invoke(); // 버튼처럼 호출
    }
}
