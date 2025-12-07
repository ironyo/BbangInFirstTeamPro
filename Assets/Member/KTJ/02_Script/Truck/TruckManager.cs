using System;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoSingleton<TruckManager>
{
    [SerializeField] private GameObject truckBody;
    [SerializeField] private Transform truckBodySpawnTran;

    private List<GameObject> _truckBodyList = new List<GameObject>();
    private int _maxTruckCount = 10;

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

    private void Start()
    {
        CurTruckCount++;
    }

    private void AddTruckBody()
    {
        GameObject _clonedBody = Instantiate(truckBody, truckBodySpawnTran);
        _truckBodyList.Add(_clonedBody);

        _clonedBody.transform.localPosition = new Vector3(((_truckBodyList.Count - 1) * -2.78f), 0, 0);
    }
}
