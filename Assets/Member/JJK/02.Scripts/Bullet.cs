using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _lifeTime = 3f;
    private Rigidbody2D _rb;
    private bool _throughFire;
    private BulletDataSO _bulletData;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(LifeCoroutine());
    }
    
    public void SetData(BulletDataSO bulletData, bool throughFire)
    {
        _bulletData = bulletData;
        _throughFire = throughFire;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = transform.right * _bulletData.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("collision");
            
            if (_throughFire)
                return;
                
            Destroy(gameObject);
        }
    }

    private IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
