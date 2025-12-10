using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseTextChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _changeTxt;

    private string _orizinTxt;

    private void Start()
    {
        _orizinTxt = _text.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.text = _changeTxt;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.text = _orizinTxt;
    }
}
