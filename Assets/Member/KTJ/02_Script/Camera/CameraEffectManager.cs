using Cysharp.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CameraEffectManager : MonoSingleton<CameraEffectManager>
{
    [SerializeField] private CinemachineCamera _camera;
    private CancellationTokenSource _zoomCoroutine;

    /// <summary>
    /// 카메라 줌인을 자연스럽게 이어준다.
    /// </summary>
    /// <param name="Amount"> 현재 카메라의 Size에 Amount만큼 줌인이 된다. </param>
    /// <param name="Time"> 줌인 시간. </param>
    public void CameraZoom(float amount, float duration)
    {
        _zoomCoroutine?.Cancel();
        _zoomCoroutine?.Dispose();
        _zoomCoroutine = new CancellationTokenSource();
        var token = _zoomCoroutine.Token;

        ZoomCoroutine(amount, duration, token).Forget();
    }


    /// <summary>
    /// 카메라의 위치를 목표지점으로 자연스럽게 이동시켜준다.
    /// </summary>
    /// <param name="Target"> 타겟 오브젝트를 설정한다. </param>
    public void CameraMoveTarget(GameObject Target)
    {
        _camera.Target.TrackingTarget = Target.transform;
    }

    private async UniTask ZoomCoroutine(float amount, float duration, CancellationToken token)
    {
        float startSize = _camera.Lens.OrthographicSize;
        float targetSize = amount;
        float t = 0f;

        while (t < 1f)
        {
            token.ThrowIfCancellationRequested();

            t += Time.deltaTime / duration;
            _camera.Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, t);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        _camera.Lens.OrthographicSize = targetSize;
    }
}