using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

public class PersonGenerator : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private Transform _targetPos;
    [Header("Prefabs")]
    [SerializeField] private GameObject _mechanic;

    public Action OnSavePointStart;

    private List<AgentMovement> _spawnedPerson = new List<AgentMovement>();

    private void Awake()
    {
        StageManager.Instance.OnStageRoadStart += OnStageStart;
        StageManager.Instance.OnStageRoadEnd += OnStageEnd;
    }

    private void OnStageEnd()
    {
        GameObject per_1 = Instantiate(_mechanic, _spawnPos);
        per_1.transform.position = _spawnPos.transform.position;

        AgentMovement am = null;

        if (per_1.TryGetComponent<AgentMovement>(out AgentMovement am_1))
        {
            am_1.MoveTo(_targetPos.position, 4f);
            am = am_1;
        }

        _spawnedPerson.Add(am);
    }

    private void OnStageStart(int a)
    {
        if (_spawnedPerson.Count > 0)
        {
            _spawnedPerson.ForEach((x) => x.MoveTo(_spawnPos.position, 7f));
            _spawnedPerson.Clear();
        }
    }
}
