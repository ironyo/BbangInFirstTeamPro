using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using UnityEngine;

public class Pepperoni : IncreaseSpeed, IRecycleObject
{
    private BulletMove bulletMove;

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private void OnEnable()
    {
        bulletMove = GetComponent<BulletMove>();
        bulletSpeed = bulletMove.Speed;
    }

    private void Update()
    {
        if (bulletMove == null)
            return;

        bulletMove.Speed = IncreaseSpeedInTime();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            Destroyed?.Invoke(this);
    }
}
