using System;
using System.Collections;
using UnityEngine;

public class JJK_Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _lifeTime = 3f;
    private float _timer = 0f;
    private bool _throughFire;
    private BulletDataSO _bulletData;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rb.linearVelocity = transform.right * _bulletData.Speed;
        _timer += Time.deltaTime;

        if (_timer > _lifeTime)
        {
            _timer = 0;
            Destroy(gameObject);
        }
    }

    public void SetData(BulletDataSO bulletData, bool throughFire)
    {
        _bulletData = bulletData;
        _throughFire = throughFire;
        _lifeTime = bulletData.LifeTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Customer"))
        {
            if (_bulletData.CollisionParticle != null)
                Instantiate(_bulletData.CollisionParticle, transform.position, Quaternion.identity);
            
            CameraShake.Instance.ImpulseForce(_bulletData.CameraShakeForce);
            
            if (_throughFire)
            {
                Debug.Log($"Damage: {_bulletData.Damage}");
                return;
            }
            else
            {
                float damage = _bulletData.Damage;
                
                if (_timer < 0.2f)
                    damage = _bulletData.Damage;
                else if (_timer < 0.5f)
                    damage = _bulletData.Damage * 0.5f;
                else
                    damage = _bulletData.Damage * 0.2f;
                
                Debug.Log($"Damage: {damage}");
            }
                
            Destroy(gameObject);
        }
    }
}
