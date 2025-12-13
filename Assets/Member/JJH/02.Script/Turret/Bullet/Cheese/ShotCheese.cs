using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class ShotCheese : TurretBase
{
    [SerializeField] private GameObject cheese;
    [SerializeField] private Transform firePos;
    Factory factory;
    private float spread = 15f;

    private void Start()
    {
        factory = new Factory(cheese, 15);
    }

    public override void Shoot()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        CameraShake.Instance.ImpulseForce(0.2f);

        SpawnCheese(rotation);
        SpawnCheese(rotation * Quaternion.Euler(0, 0, -spread));
        SpawnCheese(rotation * Quaternion.Euler(0, 0, spread));
    }

    private void SpawnCheese(Quaternion rotation)
    {
        IRecycleObject obj = factory.Get();
        obj.GameObject.transform.position = firePos.position;
        obj.GameObject.transform.rotation = rotation;
        Cheese cheese = obj.GameObject.GetComponent<Cheese>();
        cheese.damage = _damage;
    }
}
