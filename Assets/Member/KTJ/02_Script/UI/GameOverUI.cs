using DG.Tweening;
using UnityEngine;

public class GameOverUI : MonoSingleton<GameOverUI>
{
    [SerializeField] private SpriteRenderer _blackGround;
    [SerializeField] private CanvasGroup _UIs;
    [SerializeField] private Transform _truck;

    [Header("Event")]
    [SerializeField] private EventChannelSO _onGameOver;

    private void OnEnable()
    {
        _onGameOver.OnEventRaised += OnGameOver;
    }

    public void OnGameOver()
    {
        Camera cam = Camera.main;
        float screenHeight = cam.orthographicSize;

        _blackGround.gameObject.SetActive(true);
        _UIs.DOFade(0f, 1f);
        _blackGround.DOFade(1f, 2f).OnComplete(() =>
        {
            _truck.DOMoveY(-screenHeight - 2, 2f).SetEase(Ease.InOutBack);
        });
    }
}
