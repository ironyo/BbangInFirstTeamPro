using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _lifeTime = 3f;
    private float _timer = 0f;
    private Rigidbody2D _rb;
    private bool _throughFire;
    private BulletDataSO _bulletData;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _lifeTime)
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

    private void FixedUpdate()
    {
        _rb.linearVelocity = transform.right * _bulletData.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
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
