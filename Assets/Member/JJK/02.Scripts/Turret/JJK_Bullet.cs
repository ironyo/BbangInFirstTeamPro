
using System;
using System.Collections;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class JJK_Bullet : MonoBehaviour, IRecycleObject
{
    private Rigidbody2D _rb;
    private BulletDataSO _bulletData;
    private BulletMove _bulletMove;
    
    private float _lifeTime = 3f;
    private float _timer;
    private bool _throughFire;
    private float _offset = 0.5f;
    private bool _isAttack = false;

    private void Awake()
    {
        _bulletMove = GetComponentInChildren<BulletMove>();
    }

    private void OnEnable()
    {
        _timer = 0;
    }

    public Action<IRecycleObject> Destroyed { get; set; }

    public GameObject GameObject => gameObject;

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer > _lifeTime)
        {
            Destroyed?.Invoke(this);
            Instantiate(_bulletData.DisableParticle, transform.position, Quaternion.identity);
        }
    }

    public void SetData(BulletDataSO bulletData, bool throughFire)
    {
        _bulletData = bulletData;
        _throughFire = throughFire;
        _lifeTime = bulletData.LifeTime;
        _bulletMove.Speed = bulletData.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!_isAttack)
            {
                if (_bulletData.CollisionParticle != null)
                    Instantiate(_bulletData.CollisionParticle, transform.position, Quaternion.identity);
                
                collision.gameObject.GetComponent<Customer>().TakeDamage(_bulletData.Damage);
                CameraShake.Instance.ImpulseForce(_bulletData.CameraShakeForce);
            
                if (_throughFire)
                {
                    Collider2D enemyCol = collision;
                    ThroughShot(collision.transform.position + transform.up * _offset, enemyCol);
                }

                _isAttack = true;
            }
            
            Destroyed?.Invoke(this);
        }
    }

    private void ThroughShot(Vector2 shotPos, Collider2D firstEnemy)
    {
        var throughData = _bulletData.ThroughShotData;
        
        for (int i = 0; i < throughData.BulletCount; i++)
        {
            var bulletPrefab = _bulletData.ThroughShotData.BulletData.BulletPrefab;
            var bullet = BulletPoolManager.Instance.Get(bulletPrefab).GameObject;
                
            bullet.transform.position = shotPos;
            bullet.transform.rotation = transform.rotation * CalculateAngle(i);

            var tb = bullet.GetComponent<ThroughBullet>();
            tb.SetData(
                _bulletData,
                firstEnemy,
                throughData.BulletData.Speed,
                throughData.BulletData.LifeTime
            );
        }
    }
    
    private Quaternion CalculateAngle(float num)
    {
        float spreadAngle = _bulletData.ThroughShotData.SpreadAngle;

        if (_throughFire)
            spreadAngle = Mathf.Lerp(-spreadAngle, spreadAngle, num / (_bulletData.ThroughShotData.BulletCount - 1));
        
        return Quaternion.Euler(0, 0, spreadAngle);
    }

    private void OnDisable()
    {
        _isAttack = false;
    }
}
