using Assets.Member.CHG._02.Scripts.Bullet;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class BoomerangTurret : TurretBase
{
    [SerializeField] private GameObject ProjectilePrefab;
    private Factory _projectileFactory;

    private void OnEnable()
    {
        _projectileFactory = new Factory(ProjectilePrefab, 4);
    }
    public override void Shoot()
    {
        IRecycleObject obj = _projectileFactory.Get();
        obj.GameObject.GetComponent<ProjectileBase>().SetUp(_muzzle, Target);
        CameraShake.Instance.ImpulseForce(0.1f);
    }
}
