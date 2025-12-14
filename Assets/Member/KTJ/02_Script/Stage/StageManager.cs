using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    [Serializable]
    public class StageSpawnConfig
    {
        public int stage;
        public float wA, wB, wC, wD;
        public float interval;
    }

    [Header("Stage")]
    [SerializeField] private StageGenerator generator;
    [SerializeField] private int maxStage = 20;
    [SerializeField] private Canvas canvas;

    [Header("Spawn Config")]
    [SerializeField] private List<StageSpawnConfig> spawnConfigs;

    [Header("Managers")]
    [SerializeField] private CustomerSpawnManager spawnManager;

    [Header("Events")]
    [SerializeField] private EventChannelSO _onRoadFinished;
    [SerializeField] private EventChannelSO_T<int> _onStageRoadStart;
    [SerializeField] private EventChannel_TT<string, string> _setUIStage;
    [SerializeField] private EventChannelSO_T<int> _onArrivalStage;

    public int ClearStage { get; private set; }
    public bool IsRunning { get; private set; }

    private StageData current;
    private StageData previous;

    private void Start()
    {
        _onRoadFinished.OnEventRaised += EndStage;
    }

    public void StartStage()
    {
        if (IsRunning) return;
        StartCoroutine(StartStageRoutine());
    }

    private IEnumerator StartStageRoutine()
    {
        IsRunning = true;

        yield return new WaitForSeconds(1f);

        previous = ClearStage == 0 ? StageData.Create("출발지점", 0) : current;
        current = generator.CreateRandomStage();

        _onStageRoadStart.RaiseEvent(current.RoadTotalLength);
        _setUIStage.RaiseEvent(previous.Name, current.Name);
    }

    private void EndStage()
    {
        if (!IsRunning) return;

        IsRunning = false;
        ClearStage++;

        ApplySpawnConfig(ClearStage);
        _onArrivalStage.RaiseEvent(ClearStage);

        if (ClearStage >= maxStage)
        {
            canvas.gameObject.SetActive(false);
            SceneLoadManager.Instance.SceneMove(2);
        }
    }

    private void ApplySpawnConfig(int stage)
    {
        var config = spawnConfigs.Find(c => c.stage == stage);
        if (config == null) return;

        spawnManager.ApplySpawnSetting(
            config.wA,
            config.wB,
            config.wC,
            config.wD,
            config.interval
        );
    }
}
