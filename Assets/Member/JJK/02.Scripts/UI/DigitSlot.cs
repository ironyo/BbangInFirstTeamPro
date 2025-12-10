using DG.Tweening;
using TMPro;
using UnityEngine;

public class DigitSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oldDigit;
    [SerializeField] private TextMeshProUGUI newDigit;
    [SerializeField] private float digitHeight = 60f;
    [SerializeField] private float tweenDuration = 0.2f;
    
    private Tweener _fadeTween;
    private Sequence _moveSeq;
    
    public void PlayChange(int from, int to)
    {
        _fadeTween?.Kill();
        
        oldDigit.text = from.ToString();
        newDigit.text = to.ToString();
        newDigit.gameObject.SetActive(true);
        _fadeTween = oldDigit.DOFade(0, tweenDuration).SetEase(Ease.OutCubic);

        newDigit.rectTransform.anchoredPosition = new Vector2(newDigit.rectTransform.anchoredPosition.x, -digitHeight);
        
        _moveSeq = DOTween.Sequence();
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
    
    public void SetInstant(int digit)
    {
        oldDigit.text = digit.ToString();
        oldDigit.alpha = 1;

        oldDigit.rectTransform.anchoredPosition = new Vector2(oldDigit.rectTransform.anchoredPosition.x, 0);
        newDigit.rectTransform.anchoredPosition = new Vector2(newDigit.rectTransform.anchoredPosition.x, -digitHeight);

        newDigit.gameObject.SetActive(false);
    }
}
