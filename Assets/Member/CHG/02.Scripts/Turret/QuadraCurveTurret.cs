using Assets.Member.CHG._02.Scripts.Pooling;
using DG.Tweening;
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
    //private void BoomerangShoot()
    //{
    //GameObject projectile = GameObject.Instantiate(ProjectilePrefab[0], SkillSpawnPoint.position, Quaternion.identity);
    //projectile.GetComponent<ProjectileBase>().SetUp(transform, Target);
    //}

    //private void DoubleCurveShoot()
    //{
    //IRecycleObject recycleObject1 = PoolManager.Instance..Get();
    //ProjectileCurve projectile1 = recycleObject1.GameObject.GetComponent<ProjectileCurve>();

    //projectile1.Angle = 45f;
    //float angle = projectile1.Angle;

    //IRecycleObject recycleObject2 = _curveProjectileFactory.Get();
    //ProjectileCurve projectile2 = recycleObject2.GameObject.GetComponent<ProjectileCurve>();

    //projectile2.Angle = 45f;
    //projectile2.Angle = -angle; // -45f

    //projectile1.SetUp(transform, Target);
    //projectile2.SetUp(transform, Target);
    //}

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
        projectile1.SetUp(transform, Target);
        projectile2.SetUp(transform, Target);
        projectile3.SetUp(transform, Target);
        projectile4.SetUp(transform, Target);
    }

    private void ShooterShake()
    {
        transform.DOPunchPosition(-transform.right, 0.3f, 1, 1f);
    }

    public override void Shoot()
    {
        QuadraCurveShoot();
        CameraShake.Instance.ImpulseForce(0.1f);
        ShooterShake();
    }

}

