using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] private int countTime;

    private float _countFontSize = 500;
    private float _resultFontSize = 300;

    public IEnumerator StartCountDown()
    {
        text.gameObject.SetActive(true);
        text.fontSize = _countFontSize;
        
        while (countTime > 0)
        {
            text.text = countTime.ToString();
            yield return new WaitForSeconds(1f);
            countTime--;
        }
        
        text.gameObject.SetActive(false);
    }

    public IEnumerator Result(string txt)
    {
        text.gameObject.SetActive(true);
        text.text = txt;
        text.fontSize = _resultFontSize;
        
        yield return new WaitForSeconds(1f);

        text.DOFade(0f, 1f);
    }
}
