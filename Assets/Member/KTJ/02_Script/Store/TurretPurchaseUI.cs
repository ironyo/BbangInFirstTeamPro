using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretPurchaseUI : MonoBehaviour
{
    [Header("Ui Setting")]
    [SerializeField] private RectTransform _labelSpawnPos;
    [SerializeField] private GameObject _labelPref;
    [SerializeField] private GameObject Uis;

    [Header("Event")]
    [SerializeField] private EventChannel_TT<TurretSO_TJ, Action> _setTruckLabel;
    [SerializeField] private EventChannelSO _onRepairStoreUIClose;
    [SerializeField] private EventChannelSO _onRepairStoreUIReady;
    [SerializeField] private EventChannelSO _onPageDown;

    private List<GameObject> _labels = new List<GameObject>();

    private void OnEnable()
    {
        _setTruckLabel.OnEventRaised += SetLabel;

        _onRepairStoreUIReady.OnEventRaised += () =>
        {
            Uis.gameObject.SetActive(true);
        };

        _onRepairStoreUIClose.OnEventRaised += () =>
        {
            Uis.gameObject.SetActive(false);
        };

        _onPageDown.OnEventRaised += RemoveTruckLabel;
        _onRepairStoreUIClose.OnEventRaised += RemoveTruckLabel;
    }

    private void SetLabel(TurretSO_TJ _turret, Action _act)
    {
        TurretLabel _tl = Instantiate(_labelPref, _labelSpawnPos).GetComponent<TurretLabel>();

        string _tname = _turret.TurretName;
        int _cost = _turret.TurretCost;
        Sprite _sprite = _turret.TurretImage;

        _tl.SetLabel(_tname, _cost, _sprite);

        _tl.PurchaseBtn.onClick.AddListener(() =>
        {
            _act.Invoke();
        });

        _labels.Add(_tl.gameObject);
    }

    private void RemoveTruckLabel()
    {
        _labels.ForEach((x) => Destroy(x));
        _labels.Clear();
    }
}