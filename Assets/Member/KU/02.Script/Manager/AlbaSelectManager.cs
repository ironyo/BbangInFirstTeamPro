using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions.Must;
using System;

public class AlbaSelectManager : MonoBehaviour
{
    [SerializeField] private int _nowPage = 1;
    [SerializeField] private List<AlbaSelectMenu> _albaSelectMenuObj = new(3);

    private RectTransform _rectTransform;

    private Vector2 _pagePos = new Vector2(0, 0);

    private void Awake()
    {
        AlbaSelectMenu[] alba = gameObject.GetComponentsInChildren<AlbaSelectMenu>();
        for (int i = 0; i < alba.Length; i++)
        {
            _albaSelectMenuObj.Add(alba[i]);
        }
        _rectTransform = GetComponent<RectTransform>();
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
