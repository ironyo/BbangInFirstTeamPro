using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseDeltaSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform _recTransform;
    [SerializeField] private Vector3 _changeSize;
    [SerializeField] private float _changeTime;

    private Vector3 _orizinSize;

    private void Awake()
    {
        _orizinSize = _recTransform.sizeDelta;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _recTransform.DOSizeDelta(_changeSize, _changeTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _recTransform.DOSizeDelta(_orizinSize, _changeTime);
    }
}
