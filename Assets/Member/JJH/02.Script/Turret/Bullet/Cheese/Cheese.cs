using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using UnityEngine;

public class Cheese : MonoBehaviour, IRecycleObject
{
    [SerializeField] private GameObject cheeseExplosion;

    Factory factory;

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    private void Start()
    {
        factory = new Factory(cheeseExplosion, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CameraShake.Instance.ImpulseForce(0.1f);

            IRecycleObject obj = factory.Get();
            obj.GameObject.transform.position = transform.position;
            obj.GameObject.transform.rotation = Quaternion.identity;
            Destroyed?.Invoke(this);
        }
    }
}
