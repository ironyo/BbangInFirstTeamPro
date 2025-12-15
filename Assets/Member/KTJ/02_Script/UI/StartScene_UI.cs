using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class StartScene_UI : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private List<RectTransform> hideUis = new List<RectTransform>();
    [SerializeField] private EventChannelSO _onStartSceneReady;

    private void OnEnable()
    {
        _onStartSceneReady.OnEventRaised += Starttt;
    }

    private void OnDisable()
    {
        _onStartSceneReady.OnEventRaised -= Starttt;
    }

    private void Starttt()
    {
        StartCoroutine(StartUI());
    }

    private IEnumerator StartUI()
    {
        foreach(RectTransform rect in hideUis)
        {
            float outsideX = -(canvas.rect.width / 2) - (rect.rect.width / 2);
            Vector2 movePosition = new Vector2(outsideX, rect.anchoredPosition.y);

            rect.DOAnchorPos(movePosition, 1f).SetEase(Ease.InOutBack);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
