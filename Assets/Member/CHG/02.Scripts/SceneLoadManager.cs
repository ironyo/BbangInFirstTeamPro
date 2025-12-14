using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    [SerializeField] private RectTransform _settingBtn;
    
    private FadeOut _fadeOut;
    private void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(Instance);
        _fadeOut = GetComponentInChildren<FadeOut>();
    }
    public void SceneMove(int index)
    {
        Sequence seq = DOTween.Sequence();
        _settingBtn.DOAnchorPosY(100,0.3f).SetUpdate(true).SetEase(Ease.OutSine);
        _fadeOut.FadeSet(0, 1.1f).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(index));
        
        SceneManager.sceneLoaded += SceneLoaded;
    }
    public void SceneMove(string name)
    {
        Sequence seq = DOTween.Sequence();
        _settingBtn.DOAnchorPosY(100, 0.3f).SetUpdate(true).SetEase(Ease.OutSine);
        _fadeOut.FadeSet(0, 1.1f).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(name));

        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        _fadeOut.FadeSet(35, 1.1f).OnComplete(() => _settingBtn.DOAnchorPosY(-15, 0.3f).SetUpdate(true).SetEase(Ease.OutSine));
    }

    //private IEnumerator SceneChanged()
    //{
    //    //yield return new 
    //}

}
