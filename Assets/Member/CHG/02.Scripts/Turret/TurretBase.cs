using DG.Tweening;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    private bool _targetingClosed = true;
    private float _attackRange;
    private float _cooldownTime = 2f;

    private float _t;
    protected Transform Target;
    protected GunDataSO _gunData;
    public LayerMask CustomerLayer = 7;
    private bool IsSkillAcailable => (_t > _cooldownTime);
    [SerializeField] protected Transform muzzle;
    private Vector3 startPos;

    //protected Factory _projectileFactory;

    public void SpawnTurret(Transform _spawnParent)
    {
    }

    public void DeleteTurret()
    {
        Destroy(gameObject);
    }

    public void Init(TurretSO turretData)
    {
        _targetingClosed = turretData.TargetingClosedEnemy;
        _attackRange = turretData.AttackRange;
        _cooldownTime = turretData.AttackCoolTime;

        //_projectileFactory = new Factory(_projectileSO.ProjectilePrefab, _projectileSO.PoolSize);
        //PoolManager.Instance.RegisterPool(_projectileSO.ProjectilePrefab, _projectileSO.PoolSize);
    }

    public void Init2(GunDataSO gunData)
    {
        _gunData = gunData;
        _attackRange = gunData.AttackRange;
        _cooldownTime = gunData.CoolDown;
    }

    protected void Update()
    {

        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, _attackRange, CustomerLayer);

        if (enemys.Length <= 0) return;

        if (_targetingClosed)
        {
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
        }
        else
        {
            Transform farTarget = null;
            float longDistance = 0;
            foreach (var item in enemys)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance > longDistance)
                {
                    longDistance = distance;
                    farTarget = item.gameObject.transform;
                }
            }
            Target = farTarget;
        }

        Vector3 dir = Target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _t += Time.deltaTime;
        if (IsSkillAcailable)
        {
            muzzle.transform.DOLocalMove(startPos + new Vector3(0, -0.5f, 0), 0.05f)
            .OnComplete(() =>
                muzzle.transform.DOLocalMove(startPos, 0.1f)
            );

            Shoot();
            _t = 0;
        }


    }

    public abstract void Shoot();

    private void OnEnable()
    {
        startPos = muzzle.transform.localPosition;
    }




    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
