using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class DoubleCurveShooter : TurretBase
{
    [SerializeField] private GameObject ProjectilePrefab;
    private Factory _projectileFactory;

    private void OnEnable()
    {
        _projectileFactory = new Factory(ProjectilePrefab, 4);
    }

    public override void Shoot()
    {
        IRecycleObject projectile1Recycle = _projectileFactory.Get();
        ProjectileCurve projectile1 = projectile1Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile1.Angle = 45f;
        float angel = projectile1.Angle;


        IRecycleObject projectile2Recycle = _projectileFactory.Get();
        ProjectileCurve projectile2 = projectile2Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile2.Angle = -angel;

        projectile1.SetUp(muzzle, Target);
        projectile2.SetUp(muzzle,Target);
    }
}
