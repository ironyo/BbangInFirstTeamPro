using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoSingleton<GameOverUI>
{
    [SerializeField] private SpriteRenderer _blackGround;
    [SerializeField] private CanvasGroup _UIs;
    [SerializeField] private Transform _truck;
    [SerializeField] private CanvasGroup _gameOverUI;
    [SerializeField] private TextMeshProUGUI _gameOverTxt;

    [Header("Event")]
    [SerializeField] private EventChannelSO _onGameOver;

    public bool IsGameOver { get; private set; } = false;


    private void OnEnable()
    {
        _onGameOver.OnEventRaised += OnGameOver;
    }

    private void OnDisable()
    {
       _onGameOver.OnEventRaised -= OnGameOver;
    }

    public void OnGameOver()
    {
        int _clear = StageManager.Instance.ClearStage;

        Camera cam = Camera.main;
        float screenHeight = cam.orthographicSize;

        IsGameOver = true;

        _blackGround.gameObject.SetActive(true);
        _UIs.DOFade(0f, 1f);
        _blackGround.DOFade(1f, 2f).OnComplete(() =>
        {
            _truck.DOMoveY(-screenHeight - 2, 2f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                _gameOverUI.gameObject.SetActive(true);
                _gameOverTxt.text = "총 " + _clear.ToString() + "개의 마을을 통과했습니다.";
                _gameOverUI.DOFade(1f, 1f);
            });
        });
    }

    public void BackToLobby()
    {
        SceneLoadManager.Instance.SceneMove(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
