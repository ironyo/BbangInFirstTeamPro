using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{

    
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
        _fadeOut.FadeSet(0, 1.1f).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(index));
        
        SceneManager.sceneLoaded += SceneLoaded;
        //SceneManager.LoadScene(index);
    }
    public void SceneMove(string name)
    {
        Sequence seq = DOTween.Sequence();
        _fadeOut.FadeSet(0, 1.1f).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(name));


        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        _fadeOut.FadeSet(30, 1.1f);
    }

}
