using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    private StageData _current;
    private StageData _previous;
    public bool IsRunning { get; private set; } = false;
    public int ClearStage { get; private set; } = 0;

    [Header("Setting")]
    [SerializeField] private StageGenerator _generator;
    [SerializeField] private int _maxStage;

    [Header("Event")]
    [SerializeField] private EventChannelSO _onRoadFinished;
    [SerializeField] private EventChannelSO _onStageRoadEnd;
    [SerializeField] private EventChannelSO_T<int> _onStageRoadStart;
    [SerializeField] private EventChannel_TT<string, string> _setUIStage;
    [SerializeField] private EventChannelSO_T<int> _onArrivalStage;
    [SerializeField] private StageChannelInt _stageChannelInt;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _onRoadFinished.OnEventRaised += EndStage;
        StartStage();
    }

    private void Update()
    {
        if(ClearStage >= 20)
        {
            Debug.Log("꺴음");
        }
    }

    public void OnClickNewGame()
    {
        _stageChannelInt.ResetStage();
    }

    public void StartStage()
    {
        if (IsRunning) return;

        StartCoroutine(StartStageIEnum());
    }

    IEnumerator StartStageIEnum()
    {
        CameraEffectManager.Instance.CameraZoom(7, 1f);
        CameraEffectManager.Instance.CameraMoveTarget(CameraEffectManager.Instance.CameraTarget.gameObject);
        IsRunning = true;

        yield return new WaitForSeconds(1);


        _previous = ClearStage == 0
            ? StageData.Create("출발지점", 0)
            : _current;

        _current = _generator.CreateRandomStage();   // ★ Stage 생성 책임 분리

        _onStageRoadStart.RaiseEvent(_current.RoadTotalLength);
        _setUIStage.RaiseEvent(_previous.Name, _current.Name);
    }

    public void EndStage()
    {
        if (!IsRunning) return;

        IsRunning = false;
        ClearStage++;

        if (ClearStage == _maxStage)
        {
            Debug.Log("스테이지 클리어");
        }

        CameraEffectManager.Instance.CameraZoom(5, 1f);
        TruckHealthManager.Instance.TruckHealAll();
        _onArrivalStage.RaiseEvent(ClearStage);
        _stageChannelInt.RaiseEvent();

        CameraMoverManager.Instance.LockCamMove();
    }

    public StageData GetCurrent() => _current;
    public StageData GetPrevious() => _previous;
}
