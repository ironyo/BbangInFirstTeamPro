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

            enemy.MinusHP(3);
            _bounceCount--;
            if(_bounceCount <= 0)
            {
                Destroy(gameObject);
                return;
            }
            Vector2 incoming = _rigidbodyCompo.linearVelocity.normalized;
            float angle = Random.Range(-90f, 90f);
            Vector2 reflect = Quaternion.Euler(0, 0, angle) * -incoming;

            _rigidbodyCompo.linearVelocity = reflect * _moveSpeed;
        }
    }
}