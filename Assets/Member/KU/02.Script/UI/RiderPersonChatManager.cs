using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class RiderPersonChatManager : MonoSingleton<RiderPersonChatManager>
{
    [SerializeField] RectTransform _messagePos;
    [SerializeField] TextMeshProUGUI _messageText;
    [SerializeField] RiderPersonTalkSO _soData;

    private float _currentTime;
    private float _reloadTime = 5;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _reloadTime)
        {
            _currentTime = 0;
            SetRiderText(_soData.messageList[Random.Range(0, _soData.messageList.Count-1)]);
        }
    }

    public void InputRiderText(string text)
    {
        _currentTime = 0;
        SetRiderText(text);
    }

    private void SetRiderText(string text)
    {
        _messageText.text = text;
        _messagePos.rotation = Quaternion.Euler(0f, 0f, 50f);
        _messagePos.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutQuad);
    }
}
