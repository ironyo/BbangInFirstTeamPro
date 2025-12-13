using Assets.Member.CHG._02.Scripts.Bullet;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class BoomerangTurret : TurretBase
{
    [SerializeField] private GameObject ProjectilePrefab;
    private Factory _projectileFactory;

    protected override void OnEnable()
    {
        base.OnEnable();
        _projectileFactory = new Factory(ProjectilePrefab, 4);
    }
    public override void Shoot()
    {
        IRecycleObject obj = _projectileFactory.Get();
        ProjectileBase proj = obj.GameObject.GetComponent<ProjectileBase>();
        proj.Damage = _damage;
        CameraShake.Instance.ImpulseForce(0.1f);
        proj.SetUp(_muzzle, Target);
    }
}
