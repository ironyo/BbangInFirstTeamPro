using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using System.Collections;
using UnityEngine;

public class Pepperoni : IncreaseSpeed, IRecycleObject
{
    public int damage { get; set; }
    private BulletMove bulletMove;
    private TrailRenderer trail;
    [SerializeField] private SoundDataSO soundData;

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private bool isAttack = false;

    private void OnEnable()
    {
        trail = GetComponent<TrailRenderer>();
        bulletMove = GetComponent<BulletMove>();
        bulletSpeed = 10f;
        bulletMove.Speed = 10f;
        timer = 0f;

        trail.Clear();
        trail.enabled = true;

        StartCoroutine(DeadCoroutine());
    }


    private void OnDisable()
    {
        trail.enabled = false;
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
                SoundManager.Instance.PlaySound(soundData);
                isAttack = true;
            }

            Destroyed?.Invoke(this);
        }
    }

    private IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(10f);
        Destroyed?.Invoke(this);
    }
}
