using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class DoubleCurveTurret : TurretBase
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
        IRecycleObject projectile1Recycle = _projectileFactory.Get();
        ProjectileCurve projectile1 = projectile1Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile1.Angle = 45f;
        projectile1.Damage = _damage;
        float angel = projectile1.Angle;


        IRecycleObject projectile2Recycle = _projectileFactory.Get();
        ProjectileCurve projectile2 = projectile2Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile2.Angle = -angel;
        projectile2.Damage = _damage;



        projectile1.SetUp(_muzzle, Target);
        projectile2.SetUp(_muzzle, Target);
        CameraShake.Instance.ImpulseForce(0.1f);
    }
}
