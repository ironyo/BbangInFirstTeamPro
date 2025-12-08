using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class KU_Source : MonoBehaviour
{
    [SerializeField] private Sprite _katcupSprite;

    private SpriteRenderer _spriteRendererCompo;
    private Rigidbody2D _rigidbodyCompo;

    private HashSet<KU_Enemy> insideEnemies = new HashSet<KU_Enemy>();
    private Dictionary<KU_Enemy, CancellationTokenSource> enemyTokens = new Dictionary<KU_Enemy, CancellationTokenSource>();

    private void Awake()
    {
        _rigidbodyCompo = GetComponent<Rigidbody2D>();
        _spriteRendererCompo = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        DropSource().Forget();
    }

    private async UniTask DropSource()
    {
        await UniTask.WaitForSeconds(0.5f);
        _spriteRendererCompo.sprite = _katcupSprite;
        _rigidbodyCompo.linearVelocity = Vector2.zero;
        transform.DOScale(new Vector3(2, 2, 2), 1f);
        await UniTask.WaitForSeconds(3f);
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
