using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using System.Collections;
using UnityEngine;

public class Cheese : MonoBehaviour, IRecycleObject
{
    [SerializeField] private GameObject cheeseExplosion;
    public int damage { get; set; }


    Factory factory;
    private TrailRenderer trail;

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private bool isAttack = false;

    private void Start()
    {
        factory = new Factory(cheeseExplosion, 1);
    }

    private void OnEnable()
    {
        trail = GetComponent<TrailRenderer>();

        trail.Clear();
        trail.enabled = true;

        StartCoroutine(DeadCoroutine());
    }

    private void OnDisable()
    {
        trail.enabled = false;
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!isAttack)
            {
                collision.gameObject.GetComponent<Customer>().TakeDamage(damage);
                isAttack = true;
            }

            CameraShake.Instance.ImpulseForce(0.1f);

            IRecycleObject obj = factory.Get();
            obj.GameObject.transform.position = transform.position;
            obj.GameObject.transform.rotation = Quaternion.identity;
            CheeseExplosion cheeseExplosion = obj.GameObject.GetComponent<CheeseExplosion>();
            cheeseExplosion.Play(transform.position);

            Destroyed?.Invoke(this);
        }
    }

    private IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(10f);
        Destroyed?.Invoke(this);
    }
}
