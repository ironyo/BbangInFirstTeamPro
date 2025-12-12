using System;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    private StageData _current;
    private StageData _previous;
    public bool IsRunning { get; private set; } = false;
    private int _clearStage = 0;

    [Header("Setting")]
    [SerializeField] private StageGenerator _generator;
    [SerializeField] private int _maxStage;

    [Header("Event")]
    [SerializeField] private EventChannelSO _onRoadFinished;
    [SerializeField] private EventChannelSO _onStageRoadEnd;
    [SerializeField] private EventChannelSO_T<int> _onStageRoadStart;
    [SerializeField] private EventChannel_TT<string, string> _setUIStage;
    [SerializeField] private EventChannelSO_T<int> _onArrivalStage;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _onRoadFinished.OnEventRaised += EndStage;
        StartStage();
    }

    public void StartStage()
    {
        if (IsRunning) return;

        IsRunning = true;

        _previous = _clearStage == 0
            ? StageData.Create("출발지점", 0)
            : _current;

        _current = _generator.CreateRandomStage();   // ★ Stage 생성 책임 분리

        _onStageRoadStart.RaiseEvent(_current.RoadTotalLength);
        _setUIStage.RaiseEvent(_previous.Name, _current.Name);
        CameraEffectManager.Instance.CameraZoom(7, 1f);
        CameraEffectManager.Instance.CameraMoveTarget(CameraEffectManager.Instance.CameraTarget.gameObject);
    }

    public void EndStage()
    {
        if (!IsRunning) return;

        IsRunning = false;
        _clearStage++;

        if (_clearStage == _maxStage)
        {
            Debug.Log("스테이지 클리어");
        }

        CameraEffectManager.Instance.CameraZoom(5, 1f);
        TruckHealthManager.Instance.TruckHeal();
        _onArrivalStage.RaiseEvent(_clearStage);
    }

    public StageData GetCurrent() => _current;
    public StageData GetPrevious() => _previous;
}
