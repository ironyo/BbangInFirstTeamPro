using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public enum TestProjectileType
{
    Boomerang,
    DoubleCurve,
    TripleShoot,
    QuadraShoot,
    PentaShoot
}


public class QuadraCurveTurret : TurretBase
{
    [SerializeField] private GameObject ProjectilePrefab;

    private Factory _projectileFactory;
    protected override void OnEnable()
    {
        base.OnEnable();
        _projectileFactory = new Factory(ProjectilePrefab, 4);
    }

    private void QuadraCurveShoot()
    {
        IRecycleObject projectile1Recycle = _projectileFactory.Get();
        ProjectileCurve projectile1 = projectile1Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile1.Angle = 45f;
        float angel = projectile1.Angle;


        IRecycleObject projectile2Recycle = _projectileFactory.Get();
        ProjectileCurve projectile2 = projectile2Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile2.Angle = 45f;
        projectile2.Angle = -angel;

        IRecycleObject projectile3Recycle = _projectileFactory.Get();
        ProjectileCurve projectile3 = projectile3Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile3.Angle = 45f;
        projectile3.Angle = angel - 30;

        IRecycleObject projectile4Recycle = _projectileFactory.Get();
        ProjectileCurve projectile4 = projectile4Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile4.Angle = 45f;
        projectile4.Angle = -angel + 30;

        projectile1.Damage = _damage;
        projectile2.Damage = _damage;
        projectile3.Damage = _damage;
        projectile4.Damage = _damage;

        projectile1.SetUp(transform, Target);
        projectile2.SetUp(transform, Target);
        projectile3.SetUp(transform, Target);
        projectile4.SetUp(transform, Target);
    }

    //private void ShooterShake()
    //{
    //    transform.DOPunchPosition(-transform.right, 0.3f, 1, 1f);
    //}

    public override void Shoot()
    {
        QuadraCurveShoot();
        CameraShake.Instance.ImpulseForce(0.1f);
        //ShooterShake();
    }

}

