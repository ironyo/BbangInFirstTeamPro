using System.Collections;
using UnityEngine;

public class CameraEffectManager : MonoSingleton<CameraEffectManager>
{
    [SerializeField] private Camera _camera;

    /// <summary>
    /// 카메라 줌인을 자연스럽게 이어준다.
    /// </summary>
    /// <param name="Amount"> 현재 카메라의 Size에 Amount만큼 줌인이 된다. </param>
    /// <param name="Time"> 줌인 시간. </param>
    public void CameraZoomIn(float Amount, float Time)
    {
        //_camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _camera.orthographicSize - Amount, Time);
        StartCoroutine(ZoomCoroutine(Amount, Time));
    }

    /// <summary>
    /// 카메라 줌아웃을 자연스럽게 이어준다.
    /// </summary>
    /// <param name="Amount"> 현재 카메라의 Size에 Amount만큼 줌인이 된다. </param>
    /// <param name="Time"> 줌아웃 시간. </param>
    public void CameraZoomOut(float Amount, float Time)
    {
        //_camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _camera.orthographicSize + Amount, Time);
        StartCoroutine(ZoomCoroutine(-1 * Amount, Time));
    }

    private IEnumerator ZoomCoroutine(float amount, float duration)
    {
        float startSize = _camera.orthographicSize;
        float targetSize = startSize + amount;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            _camera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }

        _camera.orthographicSize = targetSize;
    }


}
