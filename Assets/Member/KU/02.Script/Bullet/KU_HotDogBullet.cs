using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

public class KU_HotDogBullet : KU_Bullet
{
    [SerializeField] private GameObject _sourcePref;
    [SerializeField] private GameObject _explosionPref;
    [SerializeField] private GameObject _sasuagePref;

    [SerializeField] private int _sourceCount = 2;
    [SerializeField] private int damage = 1;
    [SerializeField] private Vector3 _explosionSize = new Vector3(3, 3, 3);

    private SpriteRenderer _spriteRenderer;

    private bool isAttack = false;

    private CancellationTokenSource cancellationTokenSource;


    protected override void Awake()
    {
        base.Awake();
        cancellationTokenSource = new CancellationTokenSource();
        SpawnSource(cancellationTokenSource.Token).Forget();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        BoomParticle();

        transform.DOScale(new Vector3(transform.localScale.x+0.1f, transform.localScale.y + 0.1f), 0.2f);
        await _spriteRenderer.DOFade(0, 0.2f);

        await UniTask.WaitForSeconds(0, cancellationToken: cancelToken);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<Customer>(out Customer customer))
            {
                if (!isAttack)
                {
                    GameObject obj = Instantiate(_explosionPref, transform.position, Quaternion.identity);
                    obj.transform.localScale = _explosionSize;
                    GameObject sasuage = Instantiate(_sasuagePref, transform.position, Quaternion.identity);
                    Rigidbody2D rb = sasuage.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = new Vector2(1,1) * moveSpeed;

                    customer.TakeDamage(damage);
                    NowDeadTime(cancellationTokenSource.Token).Forget();
                    isAttack = true;
                }
            }
        }
    }

    private void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
    }
}