using TMPro;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _previousStageTxt;
    [SerializeField] private TextMeshProUGUI _currentStageTxt;
    [SerializeField] private EventChannel_TT<string, string> _setUIStage;

    [Header("ArrivalUISetting")]
    [SerializeField] private EventChannelSO_T<int> _onArrivalStage;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _stageTxt;
    [SerializeField] private TextMeshProUGUI _randomTxt;

    [SerializeField] private string[] _cheerRandomWords;

    private void OnEnable()
    {
        _setUIStage.OnEventRaised += SetStageUI;
        _onArrivalStage.OnEventRaised += OnArrivalStage;
    }

    public void SetStageUI(string _pre, string _cur)
    {
        _previousStageTxt.text = _pre;
        _currentStageTxt.text = _cur;
    }

    private void OnArrivalStage(int cur)
    {
        _animator.SetTrigger("OnStageArrival");
        _stageTxt.text = cur.ToString() + "번째 마을 통과!";
        _randomTxt.text = _cheerRandomWords[Random.Range(0, _cheerRandomWords.Length)];
    }

}
