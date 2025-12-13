using DG.Tweening;
using TMPro;
using UnityEngine;

public class StartFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private CanvasGroup _texts;
    [SerializeField] private GameObject MainUI;
    [SerializeField] private float _showTime;
    private bool _hide = false;
    private void Start()
    {
        _texts.DOFade(1, 0.6f);
        MainUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        _showTime -= Time.deltaTime;

        if (_showTime < 0 && _hide == false)
        {
            _hide = true;

            Sequence seq = DOTween.Sequence();

            seq.Append(_canvasGroup.DOFade(0, 0.8f));
            //seq.AppendCallback(() => MainUI.SetActive(true));
            MainUI.SetActive(true);

        }
    }

}
