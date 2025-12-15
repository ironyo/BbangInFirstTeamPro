using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private EventChannelSO _onGameOver;

    private readonly List<(AgentMovement, Person)> _spawnedPerson = new();

    // 람다 대신 "저장된" 액션을 써야 -=(해제)가 됨
    private Action _onGameOverAction;

    private void Awake()
    {
        _onGameOverAction = DeletePersonAll;
    }

    private void OnEnable()
    {
        // 구독은 OnEnable에서, 해제는 OnDisable에서 (수명주기 맞추기)
        if (_onStageRoadStart != null) _onStageRoadStart.OnEventRaised += OnStageStart;
        if (_onStageRoadEnd != null) _onStageRoadEnd.OnEventRaised += OnStageEnd;
        if (_onGameOver != null) _onGameOver.OnEventRaised += _onGameOverAction;
    }

    private void OnDisable()
    {
        // 반드시 해제
        if (_onStageRoadStart != null) _onStageRoadStart.OnEventRaised -= OnStageStart;
        if (_onStageRoadEnd != null) _onStageRoadEnd.OnEventRaised -= OnStageEnd;
        if (_onGameOver != null) _onGameOver.OnEventRaised -= _onGameOverAction;

        StopAllCoroutines();
    }

    private void Start()
    {
        OnStageEnd();
    }

    private void OnStageEnd()
    {
        // GameOverUI 인스턴스가 없을 수도 있으니 안전 체크
        if (GameOverUI.Instance != null && GameOverUI.Instance.IsGameOver) return;

        // 비활성/파괴중이면 코루틴 시작 금지
        if (!isActiveAndEnabled) return;

        StartCoroutine(OnStageEndIEnum());
    }

    private void DeletePersonAll()
    {
        for (int i = 0; i < _spawnedPerson.Count; i++)
        {
            var (am, person) = _spawnedPerson[i];

            // 이미 Destroy된 애들 안전 처리
            if (am != null) Destroy(am.gameObject);
            else if (person != null) Destroy(person.gameObject);
        }
        _spawnedPerson.Clear();
    }

    private IEnumerator OnStageEndIEnum()
    {
        if (_spawnedPerson.Count != 0)
            DeletePersonAll();

        if (_mechanic == null || _spawnPos == null || _targetPos == null)
            yield break;

        GameObject per_1 = Instantiate(_mechanic, _spawnPos);
        per_1.transform.position = _spawnPos.position;

        per_1.TryGetComponent(out AgentMovement am);
        per_1.TryGetComponent(out Person person);

        if (am != null)
            am.MoveTo(_targetPos.position, 4f);

        if (per_1.TryGetComponent(out Mechanic mec))
        {
            var btn = mec.GetReadyBtn();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    if (StageManager.Instance != null)
                        StageManager.Instance.StartStage();
                });
            }
        }

        _spawnedPerson.Add((am, person));

        yield return new WaitForSeconds(2f);

        // 2초 사이에 파괴될 수도 있으니 체크
        if (person != null)
            person.Clicked();
    }

    private void OnStageStart(int a)
    {
        for (int i = 0; i < _spawnedPerson.Count; i++)
        {
            var (am, person) = _spawnedPerson[i];

            if (am != null && _spawnPos != null)
                am.MoveTo(_spawnPos.position, 7f);

            if (person != null)
                person.UnClicked();
        }
    }
}
