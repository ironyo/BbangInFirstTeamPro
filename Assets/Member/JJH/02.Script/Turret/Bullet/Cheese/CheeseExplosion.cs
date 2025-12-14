using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using UnityEngine;

public class CheeseExplosion : MonoBehaviour, IRecycleObject
{
    [SerializeField] private GameObject cheesePuddle;
    [SerializeField] private GameObject cheeseExplosionParticle;
    [SerializeField] private SoundDataSO soundData;

    Factory factory;

    private void Start()
    {
        factory = new Factory(cheesePuddle, 1);
    }

    public void Play(Vector3 pos)
    {
        transform.position = pos;

        SoundManager.Instance.PlaySound(soundData);

        GameObject particle = Instantiate(cheeseExplosionParticle, pos, Quaternion.identity);
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
