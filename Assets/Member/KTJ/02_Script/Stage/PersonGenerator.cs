using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PersonGenerator : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private Transform _targetPos;
    [Header("Prefabs")]
    [SerializeField] private GameObject _mechanic;
    [Header("Event")]
    [SerializeField] private EventChannelSO_T<int> _onStageRoadStart;
    [SerializeField] private EventChannelSO _onStageRoadEnd;

    private List<(AgentMovement, Person)> _spawnedPerson = new List<(AgentMovement, Person)>();

    private void Awake()
    {
        _onStageRoadStart.OnEventRaised += OnStageStart;
        _onStageRoadEnd.OnEventRaised += OnStageEnd;
    }

    private void OnStageEnd()
    {
        StartCoroutine(OnStageEndIEnum());
    }
    IEnumerator OnStageEndIEnum()
    {
        GameObject per_1 = Instantiate(_mechanic, _spawnPos);
        per_1.transform.position = _spawnPos.transform.position;

        AgentMovement am = null;
        Person pers = null;

        if (per_1.TryGetComponent<AgentMovement>(out AgentMovement am_1))
        {
            am_1.MoveTo(_targetPos.position, 4f);
            am = am_1;
        }
        if (per_1.TryGetComponent<Person>(out Person per))
        {
            pers = per;
        }
        if (per_1.TryGetComponent<Mechanic>(out Mechanic mec))
        {
            mec.GetReadyBtn().onClick.AddListener(() =>
            {
                StageManager.Instance.StartStage();
            });
        }
        _spawnedPerson.Add((am, pers));

        yield return new WaitForSeconds(2f);

        per.Clicked();
    }

    private void OnStageStart(int a)
    {
        if (_spawnedPerson.Count > 0)
        {
            _spawnedPerson.ForEach((x) => x.Item1.MoveTo(_spawnPos.position, 7f));
            _spawnedPerson.ForEach((x) => x.Item2.UnClicked());
            _spawnedPerson.Clear();
        }
    }
}
