using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using System.Collections;
using UnityEngine;

public class Pepperoni : IncreaseSpeed, IRecycleObject
{
    [SerializeField] private int damage = 1;
    private BulletMove bulletMove;

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private bool isAttack = false;

    private void OnEnable()
    {
        bulletMove = GetComponent<BulletMove>();
        bulletSpeed = bulletMove.Speed;
    }

    private void OnDisable()
    {
        isAttack = false;
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
        {
            if (!isAttack)
            {
                collision.gameObject.GetComponent<Customer>().TakeDamage(damage);
                StartCoroutine(AttackCooltimeCoroutine());
            }

            Destroyed?.Invoke(this);
        }
    }

    private IEnumerator AttackCooltimeCoroutine()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.01f);
        isAttack = false;
    }
}
