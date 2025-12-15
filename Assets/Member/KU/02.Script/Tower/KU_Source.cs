using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KU_Source : KU_Bullet
{
    [SerializeField] private bool _isOnly;
    [SerializeField] private Sprite _katcupSprite;
    [SerializeField] private GameObject _trail;

    private bool isAttack = false;


    private SpriteRenderer _spriteRendererCompo;

    private HashSet<Customer> insideEnemies = new HashSet<Customer>();
    private Dictionary<Customer, CancellationTokenSource> enemyTokens = new Dictionary<Customer, CancellationTokenSource>();

    private bool _isStop = false;

    protected override void Awake()
    {
        base.Awake();
        _spriteRendererCompo = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _rigidbodyCompo.linearVelocity = _rigidbodyCompo.linearVelocity.normalized * 10;
        if (!_isOnly)
        {
            StartCoroutine(DropSource());
            DOTween.To(() => _rigidbodyCompo.linearVelocity.magnitude, s =>
            {
                _rigidbodyCompo.linearVelocity = _rigidbodyCompo.linearVelocity.normalized * s;
            }, 2, 0.5f);
        }

        RotationStop();
    }
    public override void Update()
    {
        if (_isStop)
            _rigidbodyCompo.linearVelocity = new Vector2(-10f, 0);
    }

    private IEnumerator DropSource()
    {
        yield return new WaitForSeconds(0.5f);
        StopSource();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private async UniTask StartAttackLoop(Customer customer, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.WaitForSeconds(1f, cancellationToken: token);
            customer.TakeDamage(damage);
            Debug.Log($"Attack: {customer.name}");
        }
    }

    private void StopSource()
    {
        _spriteRendererCompo.sprite = _katcupSprite;
        StopBullet();
        transform.DOScale(new Vector3(2, 2, 2), 1f);
        _isStop = true;
        Destroy(_trail);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<Customer>(out Customer customer))
            {
                SoundManager.Instance.PlaySound(soundData);
                customer.TakeDamage(damage);

                if (!isAttack)
                {
                    if (insideEnemies.Add(customer))
                    {
                        var cts = new CancellationTokenSource();
                        enemyTokens[customer] = cts;
                        StartAttackLoop(customer, cts.Token).Forget();
                    }

                    if (customer != targetEnemy) return;

                    StartCoroutine(DropSource());
                    StopSource();
                    isAttack = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Customer>(out Customer customer))
        {
            if (insideEnemies.Remove(customer))
            {
                enemyTokens[customer].Cancel();
                enemyTokens.Remove(customer);
            }
        }
    }
}
