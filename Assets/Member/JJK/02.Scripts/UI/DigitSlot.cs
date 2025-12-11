using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DigitSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oldDigit;
    [SerializeField] private TextMeshProUGUI newDigit;
    [SerializeField] private float digitHeight = 60f;
    [SerializeField] private float tweenDuration = 0.2f;

    public bool isDigit { get; set; } = false;
    
    private Tweener _fadeTween;
    private Sequence _rollSequence;
    
    public void PlayChange(int from, int to, bool plusDigit)
    {
        _fadeTween?.Kill();
    
        if (plusDigit)
        {
            oldDigit.text = from.ToString();
            isDigit = true;
        }
        else
        {
            oldDigit.text = " ";
            isDigit = true;
        }
        
        newDigit.text = to.ToString();
        newDigit.gameObject.SetActive(true);
        _fadeTween = oldDigit.DOFade(0, tweenDuration).SetEase(Ease.OutCubic);
    
        newDigit.rectTransform.anchoredPosition = new Vector2(newDigit.rectTransform.anchoredPosition.x, -digitHeight);
        
        Sequence _moveSeq = DOTween.Sequence();
        _moveSeq.Join(oldDigit.rectTransform.DOAnchorPosY(digitHeight, tweenDuration).SetEase(Ease.OutCubic));
        _moveSeq.Join(newDigit.rectTransform.DOAnchorPosY(0, tweenDuration).SetEase(Ease.OutCubic));
    
        _moveSeq.OnComplete(() =>
        {
            // 최종 숫자 정리
            oldDigit.text = to.ToString();
            oldDigit.rectTransform.anchoredPosition = new Vector2(oldDigit.rectTransform.anchoredPosition.x, 0);
            newDigit.rectTransform.anchoredPosition = new Vector2(newDigit.rectTransform.anchoredPosition.x, -digitHeight);
            oldDigit.alpha = 1;
            newDigit.gameObject.SetActive(false);
        });
    }
    
    public void SetEmpty()
    {
        oldDigit.text = "";
        newDigit.text = "";
        oldDigit.alpha = 0;
        newDigit.gameObject.SetActive(false);
        isDigit = false;
    }
}
