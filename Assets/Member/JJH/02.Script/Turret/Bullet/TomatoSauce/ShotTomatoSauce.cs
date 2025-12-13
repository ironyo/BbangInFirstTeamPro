using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class ShotTomatoSauce : TurretBase
{
    [SerializeField] private GameObject tomatoSauce;
    [SerializeField] private Transform firePos;

    Factory factory;

    private void Start()
    {
        factory = new Factory(tomatoSauce, 5);
    }

    public override void Shoot()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        CameraShake.Instance.ImpulseForce(0.2f);

        IRecycleObject obj = factory.Get();
        obj.GameObject.transform.position = firePos.position;
        obj.GameObject.transform.rotation = rotation;
        obj.GameObject.GetComponent<TomatoSauce>().ShotTomatoSauce(Target, firePos, _damage);
    }
}
