using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoadUI : MonoBehaviour
{
    [SerializeField] private RoadManager _manager;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _leftText;

    private void Awake()
    {
        _manager.OnLeftDistanceChanged += UpdateLeftDistance;
        _manager.OnSpeedChanged += UpdateSpeedUI;
    }

    private void OnDestroy()
    {
        _manager.OnLeftDistanceChanged -= UpdateLeftDistance;
        _manager.OnSpeedChanged -= UpdateSpeedUI;
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
