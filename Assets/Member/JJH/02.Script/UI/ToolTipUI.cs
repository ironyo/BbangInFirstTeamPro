using Assets.Member.CHG._02.Scripts.Pooling;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipUI : MonoBehaviour, IRecycleObject
{
    private RectTransform rect;
    private Image image;
    private TextMeshProUGUI text;

    private Coroutine destroyCoroutine;

    public Action<IRecycleObject> Destroyed { get; set; }

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        this.text.text = text;

        if (destroyCoroutine != null)
            StopCoroutine(destroyCoroutine);
        rect.DOKill();
        image.DOKill();

        rect.DOAnchorPos(new Vector3(0, -400, 0), 0f);
        image.DOFade(0f, 0f);

        rect.DOAnchorPos(new Vector2(0, -300), 0.25f);
        image.DOFade(1f, 0.25f);

        destroyCoroutine = StartCoroutine(TimeDestroyed());
    }

    public void Move(Vector3 pos)
    {
        rect.DOKill();
        rect.DOAnchorPos(pos, 0.25f);
    }

    public void DestroyUI()
    {
        UIDestroy(100f);
    }

    public IEnumerator TimeDestroyed()
    {
        yield return new WaitForSeconds(1.5f);
        UIDestroy(0f);
    }

    private void UIDestroy(float upTrans)
    {
        if (destroyCoroutine != null)
        {
            StopCoroutine(destroyCoroutine);
            destroyCoroutine = null;
        }
        rect.DOKill();
        image.DOKill();


        Sequence seq = DOTween.Sequence();
        seq.Join(rect.DOAnchorPos(rect.anchoredPosition + new Vector2(0, upTrans), 0.15f)
                            .SetEase(Ease.OutQuad));
        seq.Join(image.DOFade(0f, 0.2f)
                               .SetEase(Ease.OutQuad));
        seq.OnComplete(() =>
        {
            ToolTipManager.Instance.RemoveActive(this);
            Destroyed?.Invoke(this);
        });
    }
}
