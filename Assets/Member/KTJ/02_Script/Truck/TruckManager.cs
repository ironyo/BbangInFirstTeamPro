using System;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoSingleton<TruckManager>
{
    [SerializeField] private GameObject truckBody;
    [SerializeField] private Transform truckBodySpawnTran;

    private List<(TurretSpawner, TurretSO_TJ)> _truckBodyList = new List<(TurretSpawner, TurretSO_TJ)>();
    private int _maxTruckCount = 5;

    private int _curHealth = 100;
    public int CurHealth
    {
        get => _curHealth;
        private set => _curHealth = Mathf.Clamp(value, 0, 100);
    }

    private int _curTruckCount = 0;
    public int CurTruckCount
    {
        get => _curTruckCount;
        private set
        {
            if (_curTruckCount >= _maxTruckCount) return;

            int _addCount = value - _curTruckCount;
            for (int i = 0; i < _addCount; i++)
            {
                AddTruckBody();
            }

            _curTruckCount = Mathf.Clamp(value, 1, _maxTruckCount);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        CurTruckCount++;
    }

    public void AddTruck()
    {
        CurTruckCount++;
    }

    public bool CheckIsTruckFull()
    {
        if (_curTruckCount >= _maxTruckCount)
            return true;

        return false;
    }

    private void AddTruckBody()
    {
        GameObject _clonedBody = Instantiate(truckBody, truckBodySpawnTran);
        _truckBodyList.Add((_clonedBody.GetComponent<TurretSpawner>(), null));

        _clonedBody.transform.localPosition = new Vector3(((_truckBodyList.Count - 1) * -2.78f), 0, 0);

        CustomerSpawner.Instance.AddTargets(_clonedBody);
    }

    public void SetTurret(int SpawnTruckIdx, TurretSO_TJ turSO)
    {
        var old = _truckBodyList[SpawnTruckIdx - 1];

        old.Item1.SpawnTurret(turSO.Turret);

        _truckBodyList[SpawnTruckIdx - 1] = (old.Item1, turSO);
    }


    public TurretSO_TJ CheckIdxTurret(int idx)
    {
        return _truckBodyList[idx].Item2;
    }

}
