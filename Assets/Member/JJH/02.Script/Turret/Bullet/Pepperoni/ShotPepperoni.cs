using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class ShotPepperoni : TurretBase
{
    [SerializeField] private GameObject pepperoni;
    [SerializeField] private GameObject shotParticle;
    [SerializeField] private Transform firePos;
    [SerializeField] private SoundDataSO soundData;
    Factory factory;

    private void Start()
    {
        factory = new Factory(pepperoni, 10);
    }

    public override void Shoot()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        CameraShake.Instance.ImpulseForce(0.2f);

        Instantiate(shotParticle, firePos.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(soundData);

        IRecycleObject obj = factory.Get();
        obj.GameObject.transform.position = firePos.position;
        obj.GameObject.transform.rotation = rotation;
        Pepperoni pepperoni = obj.GameObject.GetComponent<Pepperoni>();
        pepperoni.damage = _damage;
    }
}
