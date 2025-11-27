using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions.Must;
using System;
using TMPro;
using System.Collections;
using DG.Tweening;

public class AlbaSelectManager : MonoBehaviour
{
    [SerializeField] private int _nowPage = 1;
    [SerializeField] private List<AlbaPlusMenu> _albaSelectMenuObj = new(3);
    [SerializeField] private TextMeshProUGUI _buyTextPref;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _nowPageTex;

    private Vector2 _pagePos = new Vector2(0, 0);

    public static AlbaSelectManager Instance { get; private set; }
    public List<string> albaSungList;
    public List<string> albaNameList;
        
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        AlbaPlusMenu[] alba = gameObject.GetComponentsInChildren<AlbaPlusMenu>();
        for (int i = 0; i < alba.Length; i++)
        {
            _albaSelectMenuObj.Add(alba[i]);
        }
    }

    private void Start()
    {
        _nowPageTex.text = $"Now Page : {_nowPage} / 3";
    }

    public void ChooseAlba()
    {
        PageReroll();
        BuyLog();
    }
    private void BuyLog()
    {
        Vector2 _target = new Vector2(transform.position.x, transform.position.y + 2);
        TextMeshProUGUI tex = Instantiate(_buyTextPref, transform.position, Quaternion.identity, transform);
        StartCoroutine(DestroyObj(tex.gameObject, 1));
    }
    
    public void PageReroll()
    {
        _albaSelectMenuObj[_nowPage-1].AlbaReroll();
    }
    private void PageMove(Vector3 moveTrf)
    {
        _nowPageTex.text = $"Now Page : {_nowPage} / 3";
        _rectTransform.DOAnchorPosX(moveTrf.x, 0.5f);
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
        PageMove(_pagePos);
    }
    private IEnumerator DestroyObj(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}