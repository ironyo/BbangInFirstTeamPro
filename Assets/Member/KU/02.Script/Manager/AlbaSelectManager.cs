using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions.Must;
using System;
using TMPro;

public class AlbaSelectManager : MonoBehaviour
{
    [SerializeField] private int _nowPage = 1;
    [SerializeField] private List<AlbaSelectMenu> _albaSelectMenuObj = new(3);

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _nowPageTex;

    private Vector2 _pagePos = new Vector2(0, 0);

    private void Awake()
    {
        AlbaSelectMenu[] alba = gameObject.GetComponentsInChildren<AlbaSelectMenu>();
        for (int i = 0; i < alba.Length; i++)
        {
            _albaSelectMenuObj.Add(alba[i]);
        }
    }

    private void Start()
    {
        _nowPageTex.text = $"Now Page : {_nowPage} / 3";
    }

    private void Update()
    {
        PageMove();
    }

    public void ChooseAlba()
    {
        
    }
    public void PageReroll()
    {
        _albaSelectMenuObj[_nowPage-1].AlbaReroll();
    }
    private void PageMove()
    {
        _nowPageTex.text = $"Now Page : {_nowPage} / 3";
        _rectTransform.anchoredPosition = Vector3.Lerp(_rectTransform.anchoredPosition, _pagePos, 0.05f);
    }
    public void Nextpage(bool isNexBt)
    {
        if (isNexBt)
        {
            if (_nowPage < 3)
                _nowPage++;
        }
        else
        {
            if (_nowPage > 1)
                _nowPage--;
        }

        _pagePos = new Vector2((_nowPage - 1) * -2100, 0);
    }
}
