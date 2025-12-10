using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class TurretGroup
{
    public int TurretCurrentLevel = 1;
    public string TurretGroupName;
    public List<TurretSO_TJ> Turrets = new List<TurretSO_TJ>();

    public Action OnLabelClick;
}
public class TurretPurchase : MonoBehaviour // 일단은 구매 씬 들어갈때 세팅을 못 해주는 것 같음.
{
    [Header("TurretSO")]
    [SerializeField] private List<TurretGroup> _turrets = new();

    [Header("Event")]
    [SerializeField] private EventChannelSO _onRepairStoreUIReady;

    [SerializeField] private EventChannel_TT<TurretSO_TJ, Action> _setTruckLabel;
    [SerializeField] private EventChannelSO_T<TurretSO_TJ> _onLabelClick;
    [SerializeField] private EventChannel_TT<TurretSO_TJ, int> _onTryPurchase;
    [SerializeField] private EventChannel_TT<TurretSO_TJ, int> _setTurretOnTruck;
    [SerializeField] private EventChannelSO _onPageDown;
    [SerializeField] private Button _pageDownBtn;

    [SerializeField] private TextMeshProUGUI _turretName;

    private int _currentPage = 0;
    private int _maxPage;

    private void Awake()
    {
        _maxPage = _turrets.Count - 1;
    }

    private void Start()
    {
        _pageDownBtn.onClick.AddListener(() =>
        {
            PageDown();
        });
    }
    private void OnEnable()
    {
        _onRepairStoreUIReady.OnEventRaised += SetTruckLabelUI;
        _onTryPurchase.OnEventRaised += TryPurchaseTur;
    }

    private void TryPurchaseTur(TurretSO_TJ turSO, int truckNum)
    {
        Debug.Log(turSO.TurretCost + "를 지불하고 " + turSO.TurretName + "을 구매함.");

        // 만약 돈이 부족하다면 -> return;

        _setTurretOnTruck.RaiseEvent(turSO, truckNum);
        TruckManager.Instance.SetTurret(truckNum, turSO);
    }
    
    private void SetTruckLabelUI() // 정비소 UI 준비될떄마다 호출
    {
        TurretGroup _currentPageG = _turrets[_currentPage];
        _turretName.text = _currentPageG.TurretGroupName;
        for (int i = 0; i < _currentPageG.Turrets.Count; i++)
        {

            TurretSO_TJ _tso = _currentPageG.Turrets[i];
            Action _onLabelDrag = () =>
            {
                LabelDrag(_tso);
            };

            _setTruckLabel.RaiseEvent(_tso, _onLabelDrag);
        }
    }

    private void PageDown()
    {
        Debug.Log("aa");
        _currentPage++;
        if (_maxPage < _currentPage)
        {
            _currentPage = 0;
        }
        _onPageDown.RaiseEvent();
        SetTruckLabelUI();
    }

    private void LabelDrag(TurretSO_TJ _tur)
    {
        Debug.Log(_tur.TurretName + " 클릭함");
        _onLabelClick.RaiseEvent(_tur);
    }
}