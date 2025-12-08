using DG.Tweening;
using UnityEngine;

public class KU_SausageBullet : KU_Bullet
{
    [SerializeField] private int _bounceCount = 2;

    private int _startCount = 0;
    protected override void Awake()
    {
        base.Awake();
        _startCount = _bounceCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KU_Enemy>(out KU_Enemy enemy))
        {
            if (enemy != targetEnemy && _startCount == _bounceCount) return;

            StartCoroutine(LifeTime());
            enemy.MinusHP(3);
            _bounceCount--;
            transform.DOScale(new Vector3(transform.lossyScale.x-1f/(float)(_startCount-1), transform.lossyScale.y-1f/(float)(_startCount-1)), 0.3f);
            BoomParticle();
            if(_bounceCount <= 0)
            {
                Destroy(gameObject);
                return;
            }
            Vector2 incoming = _rigidbodyCompo.linearVelocity.normalized;
            float angle = Random.Range(-90f, 90f);
            Vector2 reflect = Quaternion.Euler(0, 0, angle) * -incoming;

            _rigidbodyCompo.linearVelocity = reflect * moveSpeed;
        }
    }
}