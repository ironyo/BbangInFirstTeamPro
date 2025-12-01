using Cysharp.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CameraEffectManager : MonoSingleton<CameraEffectManager>
{
    [SerializeField] private Camera _camera;
    private CancellationTokenSource _zoomCoroutine;
    private CancellationTokenSource _moveCoroutine;

    /// <summary>
    /// 카메라 줌인을 자연스럽게 이어준다.
    /// </summary>
    /// <param name="Amount"> 현재 카메라의 Size에 Amount만큼 줌인이 된다. </param>
    /// <param name="Time"> 줌인 시간. </param>
    public void CameraZoom(float Amount, float Time)
    {
        _zoomCoroutine?.Cancel();
        _zoomCoroutine?.Dispose();
        _zoomCoroutine = new CancellationTokenSource();

        ZoomCoroutine(Amount, Time, _zoomCoroutine.Token).Forget();
    }

    /// <summary>
    /// 카메라의 위치를 목표지점으로 자연스럽게 이동시켜준다.
    /// </summary>
    /// <param name="Target"> 목표 지점을 Vector3 형식으로 지정한다..  </param>
    /// <param name="Time"> 카메라 이동 시간을 지정한다. </param>
    public void CameraMoveTarget(Vector3 Target, float Time)
    {
        _moveCoroutine?.Cancel();
        _moveCoroutine?.Dispose();
        _moveCoroutine = new CancellationTokenSource();
        TargetZoomCoroutine(Target, Time, _moveCoroutine.Token).Forget();
    }

    private async UniTask ZoomCoroutine(float amount, float duration, CancellationToken token)
    {
        float startSize = _camera.orthographicSize;
        float targetSize = amount;
        float t = 0f;

        while (t < 1f)
        {
            _zoomCoroutine.Token.ThrowIfCancellationRequested();

            t += Time.deltaTime / duration;
            _camera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        _camera.orthographicSize = targetSize;
    }


    private async UniTask TargetZoomCoroutine(Vector3 Target, float duration, CancellationToken token)
    {
        Vector2 startPos = _camera.transform.position;
        Vector2 targetPos = Target;
        float t = 0f;

        while (t < 1f)
        {
            _zoomCoroutine.Token.ThrowIfCancellationRequested();

            t += Time.deltaTime / duration;
            _camera.transform.position = Vector3.Lerp(new Vector3(startPos.x, startPos.y, -10), new Vector3(targetPos.x, targetPos.y, -10), t);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        targetPos = Target;
    }
}