using DG.Tweening;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private GameObject _fadeObj;

    public Tween FadeSet(float scale, float t)
    {
        return _fadeObj.transform.DOScale(scale, t);
    }
}
