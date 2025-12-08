using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using System.Collections;
using UnityEngine;

public class Cheese : MonoBehaviour, IRecycleObject
{
    [SerializeField] private GameObject cheeseExplosion;
    [SerializeField] private int damage = 1;

    Factory factory;

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private bool isAttack = false;

    private void Start()
    {
        factory = new Factory(cheeseExplosion, 1);
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

            CameraShake.Instance.ImpulseForce(0.1f);

            IRecycleObject obj = factory.Get();
            obj.GameObject.transform.position = transform.position;
            obj.GameObject.transform.rotation = Quaternion.identity;
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
