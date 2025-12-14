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
    [SerializeField] private int _maxStage = 20;
    [SerializeField] private Canvas _canvas;

    [Header("Event")]
    [SerializeField] private EventChannelSO _onRoadFinished;
    [SerializeField] private EventChannelSO _onStageRoadEnd;
    [SerializeField] private EventChannelSO_T<int> _onStageRoadStart;
    [SerializeField] private EventChannel_TT<string, string> _setUIStage;
    [SerializeField] private EventChannelSO_T<int> _onArrivalStage;
    [SerializeField] private StageChannelInt _stageChannelInt;

    [SerializeField] private CustomerSpawnManager customerSpawnManager;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _onRoadFinished.OnEventRaised += EndStage;
        //StartStage();

        //_onArrivalStage.RaiseEvent(ClearStage);
        //_stageChannelInt.RaiseEvent();
    }

    public void StartStage()
    {
        if (IsRunning) return;
        BGMManager.Instance.MainSound();
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

        BGMManager.Instance.FightSound();
        IsRunning = false;
        ClearStage++;

        if (ClearStage == _maxStage)
        {
            Debug.Log("스테이지 클리어");
            _canvas.gameObject.SetActive(false);
            SceneLoadManager.Instance.SceneMove(2);
        }

        CameraEffectManager.Instance.CameraZoom(5, 1f);
        TruckHealthManager.Instance.TruckHealAll();
        _onArrivalStage.RaiseEvent(ClearStage);
        _stageChannelInt.RaiseEvent();

        CameraMoverManager.Instance.LockCamMove();
        switch (ClearStage)
        {
            case 0:
                customerSpawnManager.CustomerSpawner(100, 0, 0, 0, 5);
                break;
            case 1:
                customerSpawnManager.CustomerSpawner(70, 30, 0, 0, 5);
                break;
            case 2:
                customerSpawnManager.CustomerSpawner(70, 30, 0, 0, 5);
                break;
            case 3:
                customerSpawnManager.CustomerSpawner(70, 30, 0, 0, 5);
                break;
            case 4:
                customerSpawnManager.CustomerSpawner(50, 30, 20, 0, 4.5f);
                break;
            case 5:
                customerSpawnManager.CustomerSpawner(50, 30, 20, 0, 4.5f);
                break;
            case 6:
                customerSpawnManager.CustomerSpawner(50, 30, 20, 0, 4.5f);
                break;
            case 7:
                customerSpawnManager.CustomerSpawner(50, 30, 20, 0, 4.5f);
                break;
            case 8:
                customerSpawnManager.CustomerSpawner(50, 30, 20, 0, 4.5f);
                break;
            case 9:
                customerSpawnManager.CustomerSpawner(30, 30, 20, 20, 4);
                break;
            case 10:
                customerSpawnManager.CustomerSpawner(30, 30, 20, 20, 4);
                break;
            case 11:
                customerSpawnManager.CustomerSpawner(30, 30, 20, 20, 4);
                break;
            case 12:
                customerSpawnManager.CustomerSpawner(15, 30, 25, 30, 4);
                break;
            case 13:
                customerSpawnManager.CustomerSpawner(15, 30, 25, 30, 4);
                break;
            case 14:
                customerSpawnManager.CustomerSpawner(15, 30, 25, 30, 4);
                break;
            case 15:
                customerSpawnManager.CustomerSpawner(15, 35, 15, 35, 3.75f);
                break;
            case 16:
                customerSpawnManager.CustomerSpawner(15, 35, 15, 35, 3.75f);
                break;
            case 17:
                customerSpawnManager.CustomerSpawner(15, 35, 15, 35, 3.75f);
                break;
            case 18:
                customerSpawnManager.CustomerSpawner(15, 35, 15, 35, 3.75f);
                break;
            case 19:
                customerSpawnManager.CustomerSpawner(15, 35, 15, 35, 3.75f);
                break;
        }
    }

    private void Update()
    {
    }

    public StageData GetCurrent() => _current;
    public StageData GetPrevious() => _previous;
}
