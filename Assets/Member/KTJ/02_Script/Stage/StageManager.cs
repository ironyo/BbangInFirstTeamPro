using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Road 가 시작할 때 불리는 함수
/// </summary>
/// <param name="roadLength">도로 길이</param>
public delegate void StageRoadStat(int roadLength);

public class StageManager : MonoSingleton<StageManager>
{
    private StageData _currentSD;
    private StageData _previousSD;
    public bool _isStageRunning { get; private set; } = false;

    [SerializeField] private Button _readyBtn;
    [SerializeField] private string[] _randomStageNames;
    [SerializeField] private int _maxStageRoadLength, _minStageRoadLength;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI _previousStageTxt;
    [SerializeField] private TextMeshProUGUI _currentStageTxt;

    public StageRoadStat OnStageRoadStart;
    public System.Action OnStageRoadEnd;

    private int _clearStage = 0;

    private void OnEnable()
    {
        OnStageRoadEnd += EndStage;
    }
    private void OnDisable()
    {
        OnStageRoadEnd -= EndStage;
    }

    private void Start()
    {
        _readyBtn.onClick.AddListener(StartStage);
    }
    
    private StageData PickRandomStage()
    {
        StageData newData = new StageData();
        newData.Name = _randomStageNames[Random.Range(0, _randomStageNames.Length)];
        newData.RoadTotalLength = Random.Range(_minStageRoadLength, _maxStageRoadLength);
        return newData;
    }

    public void StartStage()
    {
        if (_isStageRunning == true)
        {
            Debug.Log("이미 스테이지가 진행중이다");
            return; // 만약 스테이지가 진행중일때 또 Start 호출 -> 끝냄
        }

        Debug.Log("스테이지 시작");

        _isStageRunning = true;

        if (_clearStage == 0) // 처음 시작하는거면
        {
            _previousSD = new StageData();
            _previousSD.Name = "출발지점"; // 출발지점 세팅
        }
        else
        {
            _previousSD = _currentSD; // 클리어한 스테이지를 전 스테이지로 넘기기
        }

        _currentSD = PickRandomStage(); // 랜덤으로 새로운 스테이지를 뽑아 현재 스테이지에 저장

        OnStageRoadStart?.Invoke(_currentSD.RoadTotalLength); // 도로시작 이벤트 전송, 도로 길이와 함께

        Debug.Log("출발지점: " + _previousSD.Name + ", 종료지점: " + _currentSD.Name);
        Debug.Log("이동시간: " + _currentSD.RoadTotalLength);

        _previousStageTxt.text = _previousSD.Name;
        _currentStageTxt.text = _currentSD.Name;

        CameraEffectManager.Instance.CameraZoomIn(2, 1);
    }
    public void EndStage()
    {
        Debug.Log("스테이지 종료");

        _clearStage++; // 클리어한 스테이지 1 증가
        _isStageRunning = false; // 스테이지 진행중 = 거짓

        CameraEffectManager.Instance.CameraZoomOut(2, 1);
    }
}