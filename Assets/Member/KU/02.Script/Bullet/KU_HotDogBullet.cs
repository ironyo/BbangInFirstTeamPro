using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

public class KU_HotDogBullet : KU_Bullet
{
    [SerializeField] private Transform _particlePoint;
    [SerializeField] private GameObject _sourcePref;
    [SerializeField] private GameObject _moveParticlePref;
    [SerializeField] private GameObject _boomParticlePref;

    [SerializeField] private int _sourceCount = 2;

    //private bool _nowSourceFlag = true;

    private CancellationTokenSource cancellationTokenSource;


    protected override void Awake()
    {
        base.Awake();
        cancellationTokenSource = new CancellationTokenSource();
    }
    private void Start()
    {
        Instantiate(_moveParticlePref, _particlePoint.transform.position, Quaternion.identity, _particlePoint.transform);
        SpawnSource(cancellationTokenSource.Token).Forget();
    }


    private async UniTask SpawnSource(CancellationToken cancelToken)
    {
        try
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                float waitTime = Random.Range(0.1f, 0.3f);
                await UniTask.WaitForSeconds(waitTime, cancellationToken: cancelToken);

                if (this == null) return;
                if (transform == null) return;
                if (_sourceCount <= 0) return;

                _sourceCount--;
                GameObject obj = Instantiate(_sourcePref, transform.position, transform.rotation);

                //_nowSourceFlag = !_nowSourceFlag;

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = transform.right * moveSpeed;
            }
        }
        catch (System.OperationCanceledException)
        {

        }
    }
    private async UniTask NowDeadTime(CancellationToken cancelToken)
    {
        _rigidbodyCompo.linearVelocity = Vector2.zero;
        StopBullet();
        Instantiate(_boomParticlePref, transform.position, Quaternion.identity);


        await transform.DOScale(Vector3.zero, 0.5f);
        await UniTask.WaitForSeconds(0, cancellationToken: cancelToken);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            if (enemy != targetEnemy) return;

            enemy.MinusHP(5);
            NowDeadTime(cancellationTokenSource.Token).Forget();
        }
    }

    private void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
    }
}