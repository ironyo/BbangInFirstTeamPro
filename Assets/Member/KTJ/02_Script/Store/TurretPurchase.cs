using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretPurchase : MonoBehaviour // 일단은 구매 씬 들어갈때 세팅을 못 해주는 것 같음.
{
    [Serializable]
    public  class TurretGroup
    {
        private int TurretCurrentLevel = 0;
        public string TurretGroupName;
        public List<TurretSO_TJ> Turrets = new List<TurretSO_TJ>();

        public Action OnLabelClick;

        public void LevelUp()
        {
            if (TurretCurrentLevel == Turrets.Count) return;
            Debug.Log(TurretGroupName + "의 그룹이 업그레이드되었습니다!");
            TurretCurrentLevel++;

        }

        public int GetCurrrentLevel()
        {
            return TurretCurrentLevel;
        }
    }

    [Header("TurretSO")]
    [SerializeField] private List<TurretGroup> _turrets = new();

    [Header("Event")]
    [SerializeField] private EventChannelSO _onRepairStoreUIReady;

    [SerializeField] private EventChannel_TTT<TurretSO_TJ, Action, bool> _setTruckLabel;
    [SerializeField] private EventChannelSO_T<TurretSO_TJ> _onLabelClick;
    [SerializeField] private EventChannel_TT<TurretSO_TJ, int> _onTryPurchase;
    [SerializeField] private EventChannel_TT<TurretSO_TJ, int> _setTurretOnTruck;
    [SerializeField] private EventChannelSO _removeLabels;
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

    private TurretGroup FindTurretGroup(TurretSO_TJ _turSO)
    {
        for (int  i = 0; i < _turrets.Count; i++)
        {
            TurretGroup tg = _turrets[i];
            if (tg.Turrets.Contains(_turSO))
            {
                return tg;
            }
        }

        Debug.LogError("Group에서 SO를 찾지 못했어요! 잘가시고 ㅋ");
        return null;
    }

    private void TryPurchaseTur(TurretSO_TJ turSO, int truckNum)
    {

        // 만약 돈이 부족하다면 -> return;

        Debug.Log(turSO.TurretCost + "를 지불하고 " + turSO.TurretName + "을 구매함.");

        TurretGroup tg = FindTurretGroup(turSO);
        tg.LevelUp();

        _setTurretOnTruck.RaiseEvent(turSO, truckNum);
        TruckManager.Instance.SetTurret(truckNum, turSO);

        CameraShake.Instance.ImpulseForce(0.3f);

        SetTruckLabelUI();
    }

    private void SetTruckLabelUI() // 정비소 UI 준비될떄마다 호출
    {
        _removeLabels.RaiseEvent(); // 생성 전 라벨 삭제

        TurretGroup _currentPageG = _turrets[_currentPage];
        _turretName.text = _currentPageG.TurretGroupName;
        for (int i = 0; i < _currentPageG.Turrets.Count; i++)
        {
            TurretSO_TJ _tso = _currentPageG.Turrets[i];
            bool _isLocked = false; // 현재 잠겨있는 터렛인가
            Action _onLabelDrag = () =>
            {
                LabelDrag(_tso);
            };

            if (_currentPageG.GetCurrrentLevel() < i)
            {
                _isLocked = true;
            }

            _setTruckLabel.RaiseEvent(_tso, _onLabelDrag, _isLocked);
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
        _removeLabels.RaiseEvent();
        SetTruckLabelUI();
    }

    private void LabelDrag(TurretSO_TJ _tur)
    {
        Debug.Log(_tur.TurretName + " 클릭함");
        _onLabelClick.RaiseEvent(_tur);
    }
}