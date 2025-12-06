using TMPro;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _previousStageTxt;
    [SerializeField] private TextMeshProUGUI _currentStageTxt;
    [SerializeField] private EventChannel_TT<string, string> _setUIStage;

    private void Awake()
    {
        _setUIStage.OnEventRaised += SetStageUI;
    }

    public void SetStageUI(string _pre, string _cur)
    {
        _previousStageTxt.text = _pre;
        _currentStageTxt.text = _cur;
    }
}
