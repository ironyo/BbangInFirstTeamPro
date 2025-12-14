using Assets.Member.CHG._02.Scripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

public class RiderPersonChatManager : MonoSingleton<RiderPersonChatManager>
{
    [SerializeField] private RectTransform _spawnPos;
    [SerializeField] private RectTransform _endPos;
    [SerializeField] private GameObject _textPrefab;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private float _upvalue;
    [SerializeField, TextArea] private List<string> strings = new List<string>();
    private readonly List<BubbleChat> _activeChats = new();

    private Factory _factory;
    private float _currentTime;
    [SerializeField] private float _reloadTime = 2f;

    protected override void Awake()
    {
        base.Awake();
        _factory = new Factory(_textPrefab, 3);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _reloadTime)
        {
            _currentTime = 0;
            SetRiderText(strings[Random.Range(0, strings.Count)]);
        }
    }

    public void InputRiderText(string text)
    {
        _currentTime = 0;
        SetRiderText(text);
    }

    private void SetRiderText(string text)
    {
        // ���� ä�� ���� ���� �̵�
        for (int i = 0; i < _activeChats.Count; i++)
        {
            _activeChats[i].MoveUp(_upvalue);
        }

        IRecycleObject obj = _factory.Get();
        RectTransform rect = obj.GameObject.GetComponent<RectTransform>();
        BubbleChat chat = obj.GameObject.GetComponent<BubbleChat>();

        rect.SetParent(_canvas, false);
        rect.position = _spawnPos.position;

        chat.SetText(text, _endPos);

        _activeChats.Add(chat);

        // Ǯ�� ���ư��� ����Ʈ���� ����
        chat.Destroyed += OnChatDestroyed;
    }

    private void OnChatDestroyed(IRecycleObject obj)
    {
        BubbleChat chat = obj.GameObject.GetComponent<BubbleChat>();
        _activeChats.Remove(chat);
    }
}
