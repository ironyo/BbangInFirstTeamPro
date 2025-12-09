using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class DoubleCurveShooter : TurretBase
{


    public override void Shoot()
    {
        IRecycleObject projectile1Recycle = _projectileFactory.Get();
        ProjectileCurve projectile1 = projectile1Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile1.Angle = 45f;
        float angel = projectile1.Angle;


        IRecycleObject projectile2Recycle = _projectileFactory.Get();
        ProjectileCurve projectile2 = projectile2Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile2.Angle = -angel;
    }
}
