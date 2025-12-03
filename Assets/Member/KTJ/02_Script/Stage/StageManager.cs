using System;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    public event Action<int> OnStageRoadStart;
    public event Action<string, string> SetUIStage;
    public Action OnStageRoadEnd;


    private StageData _current;
    private StageData _previous;
    public bool IsRunning { get; private set; } = false;
    private int _clearStage = 0;

    [SerializeField] private StageGenerator _generator; // ★ 의존성 분리

    private void Start()
    {
        OnStageRoadEnd += EndStage;
    }

    public void StartStage()
    {
        if (IsRunning) return;

        IsRunning = true;

        _previous = _clearStage == 0
            ? StageData.Create("출발지점", 0)
            : _current;

        _current = _generator.CreateRandomStage();   // ★ Stage 생성 책임 분리

        OnStageRoadStart?.Invoke(_current.RoadTotalLength);
        SetUIStage?.Invoke(_previous.Name, _current.Name);
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
