using System;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    private StageData _current;
    private StageData _previous;
    public bool IsRunning { get; private set; } = false;
    private int _clearStage = 0;

    [SerializeField] private StageGenerator _generator; // ★ 의존성 분리
    [SerializeField] private EventChannelSO _onRoadFinished;
    [SerializeField] private EventChannelSO _onStageRoadEnd;
    [SerializeField] private EventChannelSO_T<int> _onStageRoadStart;
    [SerializeField] private EventChannel_TT<string, string> _setUIStage;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _onRoadFinished.OnEventRaised += EndStage;
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
        CameraEffectManager.Instance.CameraZoom(5, 1f);
    }

    public StageData GetCurrent() => _current;
    public StageData GetPrevious() => _previous;
}
