using Assets.Member.CHG._04.SO.Scripts;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public abstract class TurretBase : MonoBehaviour
{
    private bool _targetingClosed = true;
    private float _attackRange;
    private float _power;
    private float _cooldownTime = 2f;

    private float _t;
    protected Transform Target;
    protected GunDataSO _gunData;
    public LayerMask CustomerLayer = 7;
    private bool IsSkillAcailable => (_t > _cooldownTime);
    [SerializeField] protected Transform _muzzle;
    [SerializeField] private SpriteRenderer _affixSpriteRen;
    [SerializeField] private Transform _firePos;
    private Vector3 startPos;

    private LineRenderer _lineRenderer;

    public void SpawnTurret(Transform _spawnParent)
    {
    }

    public void DeleteTurret()
    {
        Destroy(gameObject);
    }

    public void Init(TurretSO turretData, AffixSO affixData)
    {
        _targetingClosed = turretData.TargetingClosedEnemy;
        _attackRange = turretData.AttackRange;
        _cooldownTime = turretData.AttackCoolTime;
        _power = turretData.AttackPower;

        AffixSet(affixData);

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _firePos.position);
    }
    public void Init(TurretSO turretData)
    {
        _targetingClosed = turretData.TargetingClosedEnemy;
        _attackRange = turretData.AttackRange;
        _cooldownTime = turretData.AttackCoolTime;
        _power = turretData.AttackPower;

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _firePos.position);
    }
    public void Init(GunDataSO gunData)
    {
        _gunData = gunData;
        _attackRange = gunData.AttackRange;
        _cooldownTime = gunData.CoolDown;
    }
    public void Init(GunDataSO gunData, AffixSO affixData)
    {
        _gunData = gunData;
        _attackRange = gunData.AttackRange;
        _cooldownTime = gunData.CoolDown;

        AffixSet(affixData);
    }


    private void AffixSet(AffixSO affixData)
    {
        _affixSpriteRen.sprite = affixData.AffixSprite;

        if (affixData != null)
        {
            switch (affixData.AffixType)
            {
                case AffixType.AddPower:
                    _power += affixData.Value;
                    break;
                case AffixType.AddRange:
                    _attackRange += affixData.Value;
                    break;
                case AffixType.LowCoolTime:
                    _cooldownTime -= affixData.Value;
                    break;
            }
        }
    }
    protected void Update()
    {
        if (_lineRenderer == null)
            return;

        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, _attackRange, CustomerLayer);

        if (enemys.Length <= 0)
        {
            _lineRenderer.SetPosition(1, _firePos.position);
        }

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

        Quaternion dir = transform.rotation;
        _lineRenderer.SetPosition(0, _firePos.position);
        if (Target == null)
        {
        }
        else
        {
            _lineRenderer.SetPosition(1, Target.position);
            dir = Quaternion.Euler(Target.position - transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }






        _t += Time.deltaTime;
        if (IsSkillAcailable)
        {
            if (Target != null)
            {
                _muzzle.transform.DOLocalMove(startPos + new Vector3(0, -0.5f, 0), 0.05f)
                .OnComplete(() =>
                    _muzzle.transform.DOLocalMove(startPos, 0.1f)
                );
                Shoot();

            }
            _t = 0;
        }


    }

    public abstract void Shoot();

    private void OnEnable()
    {
        startPos = _muzzle.transform.localPosition;
    }




    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
