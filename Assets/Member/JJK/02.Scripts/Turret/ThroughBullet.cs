using System;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class ThroughBullet : MonoBehaviour, IRecycleObject
{
    private BulletDataSO _bulletData;
    private BulletMove _bulletMove;
    private float _lifeTime;
    private float _timer;
    private bool _isAttack;
    
    private Collider2D _ignoreTarget;
    
    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private void Awake()
    {
        _bulletMove = GetComponentInChildren<BulletMove>();
    }

    private void OnEnable()
    {
        _timer = 0;
        _ignoreTarget = null;
    }

    public void SetData(BulletDataSO data, Collider2D firstEnemy, float speed, float lifeTime)
    {
        _bulletData = data;
        _lifeTime = lifeTime;

        _bulletMove.Speed = speed;

        _ignoreTarget = firstEnemy;
        
        if (_ignoreTarget != null)
        {
            Physics2D.IgnoreCollision(
                GetComponent<Collider2D>(),
                _ignoreTarget,
                true
            );
        }
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _lifeTime)
        {
            Destroyed?.Invoke(this);
            Instantiate(_bulletData.DisableParticle, transform.position, Quaternion.identity);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_ignoreTarget != null && collision == _ignoreTarget)
            return;

        if (collision.CompareTag("Enemy"))
        {
            if (!_isAttack)
            {
                collision.gameObject.GetComponent<Customer>().TakeDamage(_bulletData.Damage);
                CameraShake.Instance.ImpulseForce(_bulletData.CameraShakeForce);

                if (_bulletData.CollisionParticle != null)
                    Instantiate(_bulletData.CollisionParticle, transform.position, Quaternion.identity);

                _isAttack = true;
            }

            Destroyed?.Invoke(this);
        }
    }
}
