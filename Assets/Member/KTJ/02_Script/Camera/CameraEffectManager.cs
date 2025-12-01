using System.Collections;
using UnityEngine;

public class CameraEffectManager : MonoSingleton<CameraEffectManager>
{
    [SerializeField] private Camera _camera;
    private Coroutine _zoomCoroutine;
    private Coroutine _moveCoroutine;

    /// <summary>
    /// 카메라 줌인을 자연스럽게 이어준다.
    /// </summary>
    /// <param name="Amount"> 현재 카메라의 Size에 Amount만큼 줌인이 된다. </param>
    /// <param name="Time"> 줌인 시간. </param>
    public void CameraZoom(float Amount, float Time)
    {
        if (_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
        }
        _zoomCoroutine = StartCoroutine(ZoomCoroutine(Amount, Time));
    }

    /// <summary>
    /// 카메라의 위치를 목표지점으로 자연스럽게 이동시켜준다.
    /// </summary>
    /// <param name="Target"> 목표 지점을 Vector3 형식으로 지정한다..  </param>
    /// <param name="Time"> 카메라 이동 시간을 지정한다. </param>
    public void CameraMoveTarget(Vector3 Target, float Time)
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
        _moveCoroutine = StartCoroutine(TargetZoomCoroutine(Target, Time));
    }

    private IEnumerator ZoomCoroutine(float amount, float duration)
    {
        float startSize = _camera.orthographicSize;
        float targetSize = amount;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            _camera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }

        _camera.orthographicSize = targetSize;
    }

    private IEnumerator TargetZoomCoroutine(Vector3 Target, float duration)
    {
        Vector2 startPos = _camera.transform.position;
        Vector2 targetPos = Target;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            _camera.transform.position = Vector3.Lerp(new Vector3(startPos.x, startPos.y, -10), new Vector3(targetPos.x, targetPos.y, -10), t);
            yield return null;
        }

        targetPos = Target;
    }
}