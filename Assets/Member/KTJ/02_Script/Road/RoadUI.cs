using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoadUI : MonoBehaviour
{
    [SerializeField] private RoadManager _manager;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _leftText;

    [SerializeField] EventChannelSO_T<float> _onLeftDistanceChanged;
    [SerializeField] EventChannelSO_T<float> _onSpeedChanged;


    private void Awake()
    {
        _onLeftDistanceChanged.OnEventRaised += UpdateLeftDistance;
        _onSpeedChanged.OnEventRaised += UpdateSpeedUI;
    }

    private void OnDestroy()
    {
        //if (StageManager.Instance == null)
        //{
        //    Debug.Log("스테이지 매니저의 인스턴스가 널입니다");
        //}
        _onLeftDistanceChanged.OnEventRaised += UpdateLeftDistance;
        _onSpeedChanged.OnEventRaised += UpdateSpeedUI;
    }

    private void UpdateLeftDistance(float left)
    {
        if (_slider.maxValue < left)
            _slider.maxValue = left;

        _slider.value = _slider.maxValue - left;

        _leftText.text = $"남은거리: {left:F1}m";
    }

    private void UpdateSpeedUI(float speed)
    {
        // 필요하면 속도 UI도 여기서 변경
    }
}
