using System;
using System.Collections;
using Assets.Member.CHG._02.Scripts.Pooling;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleChat : MonoBehaviour, IRecycleObject
{
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private Image _bg;
    [SerializeField] private float _liveTime = 2f;
    [SerializeField] private float _bgHorizontalPadding = 40f;
    [SerializeField] private float _minBgWidth = 120f; 
    [SerializeField] private float _maxBgWidth = 600f; 
    private RectTransform _rect;
    private Vector2 _originPos;
    private Coroutine _returnRoutine;
    private Tween _tween;
    public bool IsMoving { get; private set; }

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void SetText(string text, RectTransform endPos)
    {
        StopAllCoroutines();
        _rect.DOKill(true);

        _tmp.text = text;
        _tmp.ForceMeshUpdate();

        _tmp.ForceMeshUpdate();

        ResizeBackground();
        _originPos = _rect.position;

        IsMoving = true;

       _tween = _rect.DOAnchorPosX(endPos.position.x, 1.3f)
             .SetEase(Ease.OutBack)
             .OnComplete(() =>
             {
                 IsMoving = false;
                 Debug.Log("aaa");
                 _returnRoutine = StartCoroutine(TextReturn());
             });
    }
    private void ResizeBackground()
    {
        RectTransform bgRect = _bg.rectTransform;

        float targetWidth = _tmp.preferredWidth + _bgHorizontalPadding;

        // 최소 / 최대 제한 (선택)
        targetWidth = Mathf.Clamp(targetWidth, _minBgWidth, _maxBgWidth);

        bgRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
    }
    public void MoveUp(float amount)
    {

        //StopAllCoroutines();
        //_rect.DOKill(true);

        IsMoving = true;

        Vector2 target = new Vector2(_rect.position.x,_rect.position.y) + Vector2.up * amount;

        _rect.DOAnchorPos(target, 0.3f)
             .SetEase(Ease.OutQuad)
             .OnComplete(() => IsMoving = false);
    }

    private IEnumerator TextReturn()
    {
        Debug.Log("bba");
        Debug.Log(_liveTime);
        yield return new WaitForSeconds(_liveTime);

        Debug.Log(IsMoving);
        IsMoving = false;
        yield return new WaitUntil(() => !IsMoving);

        _rect.DOAnchorPosX(_originPos.x, 1.3f)
             .SetEase(Ease.InBack)
             .OnComplete(() =>
             {
                 IsMoving = false;
                 Destroyed?.Invoke(this);
             });
    }
}
