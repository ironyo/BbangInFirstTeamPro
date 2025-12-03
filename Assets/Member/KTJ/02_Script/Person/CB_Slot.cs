using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CB_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Setting")]
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private RectTransform StretchRec;

    [Header("Value Setting")]
    [SerializeField] private string OnMouseEnterTxt;
    [SerializeField] private string OnMouseLeaveTxt;

    public void OnPointerEnter(PointerEventData eventData)
    {
        int _stirngLen = OnMouseEnterTxt.Length;

        Text.text = OnMouseEnterTxt;
        SetStretchRec(_stirngLen);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        int _stirngLen = OnMouseLeaveTxt.Length;

        Text.text = OnMouseLeaveTxt;
        SetStretchRec(_stirngLen);
    }

    private void SetStretchRec(int sLength)
    {
        StretchRec.DOSizeDelta(new Vector2(0.2f + sLength * 0.2f, StretchRec.sizeDelta.y), 0.5f);
    }
}
