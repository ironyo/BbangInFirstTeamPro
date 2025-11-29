using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class KU_Source : MonoBehaviour
{
    private CancellationTokenSource cancellationTokenSource;
    private Rigidbody2D _rigidbodyCompo;
    private void Awake()
    {
        _rigidbodyCompo = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        DropSource().Forget();
    }

    private async UniTask DropSource()
    {
        await UniTask.WaitForSeconds(0.5f);
        _rigidbodyCompo.linearVelocity = Vector2.zero;
        transform.DOScale(new Vector3(2, 2, 2), 1f);
        await UniTask.WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
