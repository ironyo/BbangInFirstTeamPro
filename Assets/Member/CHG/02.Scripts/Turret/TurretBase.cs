using Assets.Member.CHG._04.SO.Scripts;
using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public abstract class TurretBase : MonoBehaviour
{
    [SerializeField] protected TurretSO turretData;
    [SerializeField] private GunDataSO gunData;
    protected bool _targetingClosed = true;
    protected float _attackRange;
    protected int _damage;
    protected float _cooldownTime = 2f;
    protected float _t;
    protected float time;

    protected Transform Target;
    protected GunDataSO _gunData;
    public LayerMask CustomerLayer = 7;
    [SerializeField] protected Transform _muzzle;
    [SerializeField] private SpriteRenderer _affixSpriteRen;
    [SerializeField] protected Transform _firePos;
    [SerializeField] private GameObject _attackSpeedSliderPrefab;

    [Header("Event")]
    [SerializeField] private EventChannelSO_T<int> _onRaiseDamage;
    [SerializeField] private EventChannelSO_T<float> _onRaiseDamageTime;

    private AttackSpeedSlider _attackSpeedSlider;
    private Vector3 startPos;

    protected LineRenderer _lineRenderer;

    private bool isCooltime = false;

    private void Awake()
    {
        _onRaiseDamage.OnEventRaised += Damageup;
        _onRaiseDamageTime.OnEventRaised += x => time = x;

        if (turretData != null)
        {
            Debug.Log("터렛 베이스 데이터 들어옴");

            _targetingClosed = turretData.TargetingClosedEnemy;
            _attackRange = turretData.AttackRange;
            _cooldownTime = turretData.AttackCoolTime;
            _damage = turretData.AttackPower;

            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _firePos.position);

            MakeAttackSpeedSlider();

            _t = _cooldownTime;
            isCooltime = false;
            _attackSpeedSlider.UpdateSlider(1f);
        }
        else
        {
            Debug.Log("터렛 베이스 데이터 들어옴");

            _gunData = gunData;
            _attackRange = gunData.AttackRange;
            _cooldownTime = gunData.CoolDown;
            _damage = gunData.damage;

            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _firePos.position);

            MakeAttackSpeedSlider();

            _t = _cooldownTime;
            isCooltime = false;
            _attackSpeedSlider.UpdateSlider(1f);
        }
    }

    private void Damageup(int amount)
    {
        int added = Mathf.RoundToInt(_damage * (amount / 100f));
        _damage += added;
        StartCoroutine(DiasbleDamageUpCoroutine(added));
    }

    private IEnumerator DiasbleDamageUpCoroutine(int added)
    {
        yield return new WaitForSeconds(time);
        _damage -= added;
    }
    public void SpawnTurret(Transform _spawnParent)
    {
    }

    public void DeleteTurret()
    {
        Destroy(gameObject);
    }

    public void AffixSet(AffixSO affixData)
    {
        _affixSpriteRen.sprite = affixData.AffixSprite;

        if (affixData != null)
        {
            switch (affixData.AffixType)
            {
                case AffixType.AddPower:
                    _damage += affixData.Value;
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

    protected void MakeAttackSpeedSlider()
    {
        GameObject prefab = Instantiate(_attackSpeedSliderPrefab);
        _attackSpeedSlider = prefab.GetComponent<AttackSpeedSlider>();
        _attackSpeedSlider.Init(transform);
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

        _lineRenderer.SetPosition(0, _firePos.position);

        if (Target != null)
        {
            _lineRenderer.SetPosition(1, Target.position);

            Vector2 dir = Target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }






        if (!isCooltime && _t >= _cooldownTime && Target != null)
        {
            _muzzle.transform.DOLocalMove(startPos + new Vector3(0, -0.5f, 0), 0.05f)
                .OnComplete(() =>
                    _muzzle.transform.DOLocalMove(startPos, 0.1f)
                );
            Shoot();

            _t = 0f;
            isCooltime = true;
            _attackSpeedSlider.UpdateSlider(0f);
        }

        if (isCooltime)
        {
            _t += Time.deltaTime;

            float ratio = Mathf.Clamp01(_t / _cooldownTime);
            _attackSpeedSlider.UpdateSlider(ratio);

            if (_t >= _cooldownTime)
            {
                _t = _cooldownTime;
                isCooltime = false;
                _attackSpeedSlider.UpdateSlider(1f);
            }
        }


    }

    public abstract void Shoot();

    protected virtual void OnEnable()
    {
        startPos = _muzzle.transform.localPosition;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
