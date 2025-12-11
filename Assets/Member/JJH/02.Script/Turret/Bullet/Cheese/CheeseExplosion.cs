using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using UnityEngine;

public class CheeseExplosion : MonoBehaviour, IRecycleObject
{
    [SerializeField] private GameObject cheesePuddle;
    [SerializeField] private GameObject cheeseExplosionParticle;

    Factory factory;

    private void Start()
    {
        Instantiate(cheeseExplosionParticle, transform.position, Quaternion.identity);
        factory = new Factory(cheesePuddle, 1);
    }

    public Action<IRecycleObject> Destroyed { get; set; }
    public GameObject GameObject => gameObject;

    public void ExplosionDestroy()
    {
        IRecycleObject obj = factory.Get();
        obj.GameObject.transform.position = transform.position;
        obj.GameObject.transform.rotation = Quaternion.identity;
        Destroyed?.Invoke(this);
    }
}
