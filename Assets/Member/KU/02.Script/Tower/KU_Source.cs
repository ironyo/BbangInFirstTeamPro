using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class KU_Source : KU_Bullet
{
    [SerializeField] private bool _isOnly;
    [SerializeField] private Sprite _katcupSprite;

    private SpriteRenderer _spriteRendererCompo;

    private HashSet<KU_Enemy> insideEnemies = new HashSet<KU_Enemy>();
    private Dictionary<KU_Enemy, CancellationTokenSource> enemyTokens = new Dictionary<KU_Enemy, CancellationTokenSource>();

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

    private IEnumerator DropSource()
    {
        yield return new WaitForSeconds(0.5f);
        StopSource();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private async UniTask StartAttackLoop(KU_Enemy enemy, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.WaitForSeconds(1f, cancellationToken: token);
            enemy.MinusHP(2);
            Debug.Log($"Attack: {enemy.name}");
        }
    }

    private void StopSource()
    {
        _spriteRendererCompo.sprite = _katcupSprite;
        StopBullet();
        transform.DOScale(new Vector3(2, 2, 2), 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            if (insideEnemies.Add(enemy))
            {
                var cts = new CancellationTokenSource();
                enemyTokens[enemy] = cts;
                StartAttackLoop(enemy, cts.Token).Forget();
            }

            if (enemy != targetEnemy) return;

            StartCoroutine(DropSource());
            StopSource();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            if (insideEnemies.Remove(enemy))
            {
                enemyTokens[enemy].Cancel();
                enemyTokens.Remove(enemy);
            }
        }
    }
}
