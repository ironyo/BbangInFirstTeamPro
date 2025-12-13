using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    [Header("UI")]
    [SerializeField] private RectTransform _labelSpawnPos;
    [SerializeField] private GameObject _unlockPref;

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

    private GameObject _unlockPrefClone;

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

    private (TurretGroup, int) FindTurretGroup(TurretSO_TJ _turSO)
    {
        for (int  i = 0; i < _turrets.Count; i++)
        {
            TurretGroup tg = _turrets[i];
            if (tg.Turrets.Contains(_turSO))
            {
                int IdxNum = tg.Turrets.IndexOf(_turSO);
                return (tg, IdxNum);
            }
        }

        Debug.LogError("Group에서 SO를 찾지 못했어요! 잘가시고 ㅋ");
        return (null, -1);
    }

    private void TryPurchaseTur(TurretSO_TJ turSO, int truckNum)
    {

        // 만약 돈이 부족하다면 -> return;

        Debug.Log(turSO.TurretCost + "를 지불하고 " + turSO.TurretName + "을 구매함.");
        if (MoneyManager.Instance.SpendMoney(turSO.TurretCost) == false)
        {
            ToolTipManager.Instance.ShowToolTip("잔액이 부족합니다");
        }
        else
        {
            (TurretGroup, int) tg = FindTurretGroup(turSO);
            if (tg.Item2 == tg.Item1.GetCurrrentLevel())
            {
                tg.Item1.LevelUp();
            }

            _setTurretOnTruck.RaiseEvent(turSO, truckNum);
            TruckManager.Instance.SetTurret(truckNum, turSO);

            CameraShake.Instance.ImpulseForce(0.3f);

            SetTruckLabelUI();
        }
    }

    private void SetTruckLabelUI()
    {
        _removeLabels.RaiseEvent();

        TurretGroup group = _turrets[_currentPage];
        _turretName.text = group.TurretGroupName;

        int curLevel = group.GetCurrrentLevel();
        if (_unlockPrefClone != null) Destroy(_unlockPrefClone);

        for (int i = 0; i < group.Turrets.Count; i++)
        {
            // 현재 레벨과 다음 레벨 사이에 unlock 프리팹 삽입
            if (i - 1 == curLevel && curLevel < group.Turrets.Count)
            {
                if (_unlockPrefClone != null) Destroy(_unlockPrefClone);
                _unlockPrefClone = Instantiate(_unlockPref, _labelSpawnPos);
            }

            TurretSO_TJ tso = group.Turrets[i];
            bool isLocked = i > curLevel;

            Action onDrag = () => LabelDrag(tso);
            _setTruckLabel.RaiseEvent(tso, onDrag, isLocked);
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