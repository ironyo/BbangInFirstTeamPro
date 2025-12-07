using Assets.Member.CHG._02.Scripts.Bullet;
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


public class TestShooting : MonoBehaviour
{
    [SerializeField] private TestProjectileType _projectileType;
    [SerializeField] private float CooldownTime = 2f;
    [SerializeField] private GameObject[] ProjectilePrefab;
    [SerializeField] private Transform SkillSpawnPoint;
    [SerializeField] private Transform Target;
    public LayerMask EnemyLayer;
    public float Range;
    private float _currentCoolTime = 0;
    public bool IsSkillAcailable => (Time.time - _currentCoolTime > CooldownTime);

    private Factory _curveProjectileFactory;
    private void Start()
    {
        _curveProjectileFactory = new Factory(ProjectilePrefab[1], 5);
    }

    private void Update()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, Range, EnemyLayer);

        Transform closestTarget = null;
        float shortDistance = Mathf.Infinity;

        foreach (var item in enemys)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance < shortDistance)
            {
                shortDistance = distance;
                closestTarget = item.gameObject.transform;
            }
        }
        Target = closestTarget;

        Vector3 dir = Target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        OnSkill();
    }

    private void OnSkill()
    {
        if (!IsSkillAcailable) return;

        if (ProjectilePrefab != null)
        {
            switch (_projectileType)
            {
                case TestProjectileType.Boomerang:
                    {
                        BoomerangShoot();
                    }
                    break;
                case TestProjectileType.DoubleCurve:
                    {
                        DoubleCurveShoot();
                    }
                    break;
                case TestProjectileType.TripleShoot:
                    {
                        BoomerangShoot();
                        DoubleCurveShoot();
                    }
                    break;
                case TestProjectileType.QuadraShoot:
                    {
                        QuadraCurveShoot();
                    }
                    break;
                case TestProjectileType.PentaShoot:
                    {

                        BoomerangShoot();
                        QuadraCurveShoot();
                    }
                    break;
            }

            
        }

        _currentCoolTime = Time.time;
    }

    private void BoomerangShoot()
    {
        GameObject projectile = GameObject.Instantiate(ProjectilePrefab[0], SkillSpawnPoint.position, Quaternion.identity);
        projectile.GetComponent<ProjectileBase>().SetUp(transform, Target, 1);
    }

    private void DoubleCurveShoot()
    {
        IRecycleObject recycleObject1 = _curveProjectileFactory.Get();
        ProjectileCurve projectile1 = recycleObject1.GameObject.GetComponent<ProjectileCurve>();

        projectile1.Angle = 45f;
        float angle = projectile1.Angle;

        IRecycleObject recycleObject2 = _curveProjectileFactory.Get();
        ProjectileCurve projectile2 = recycleObject2.GameObject.GetComponent<ProjectileCurve>();

        projectile2.Angle = 45f;
        projectile2.Angle = -angle; // -45f

        projectile1.SetUp(transform, Target, 1);
        projectile2.SetUp(transform, Target, 1);
    }

    private void QuadraCurveShoot()
    {
        IRecycleObject projectile1Recycle = _curveProjectileFactory.Get();
        ProjectileCurve projectile1 = projectile1Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile1.Angle = 45f;
        float angel = projectile1.Angle; 

  
        IRecycleObject projectile2Recycle = _curveProjectileFactory.Get();
        ProjectileCurve projectile2 = projectile2Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile2.Angle = 45f;
        projectile2.Angle = -angel; 

        IRecycleObject projectile3Recycle = _curveProjectileFactory.Get();
        ProjectileCurve projectile3 = projectile3Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile3.Angle = 45f; 
        projectile3.Angle = angel - 30; 


        IRecycleObject projectile4Recycle = _curveProjectileFactory.Get();
        ProjectileCurve projectile4 = projectile4Recycle.GameObject.GetComponent<ProjectileCurve>();
        projectile4.Angle = 45f; 
        projectile4.Angle = -angel + 30; 

        projectile1.SetUp(transform, Target, 1);
        projectile2.SetUp(transform, Target, 1);
        projectile3.SetUp(transform, Target, 1);
        projectile4.SetUp(transform, Target, 1);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
