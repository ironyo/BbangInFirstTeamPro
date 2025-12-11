using DG.Tweening;
using TMPro;
using UnityEngine;

public class StartFade : MonoBehaviour
{
    [SerializeField] private GameObject FadeObj;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private GameObject MainUI;
    [SerializeField] private float _showTime;
    private bool _hide = false;
    private void Start()
    {
        Text.DOFade(1, 0.4f);
        MainUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        _showTime -= Time.deltaTime;

        if (_showTime < 0 && _hide == false)
        {
            _hide = true;

            Sequence seq = DOTween.Sequence();

            seq.Append(Text.DOFade(0, 0.4f));
            seq.Append(FadeObj.transform.DOScale(30, 0.4f));
            
        }
    }

}
