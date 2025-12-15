using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}

public class Customer : MonoBehaviour
{
    public event Action OnClearRequested;
    [SerializeField] private EventChannelSO _onGameOver;

    [Header("Gizmo Range")]
    [SerializeField] private Vector2 closeRange;
    [SerializeField] private Vector2 attackRange;

    [SerializeField] private CustomerHitParticle hitParticle;

    [Header("Targets")]
    public Transform[] runTargets;
    public Transform[] hitTargets;

    [SerializeField] private LayerMask truckMask;
    [SerializeField] private LayerMask closeMask;

    private IEnemyState currentState;

    public RunState RunState { get; private set; }
    public AttackState AttackState { get; private set; }
    public CloseState CloseState { get; private set; }
    public ClearState ClearState { get; private set; }
    public DeadState DeadState { get; private set; }

    [Header("UI")]
    public TextMeshPro hpText;
    [SerializeField] private TextMeshPro damageText;
    [SerializeField] private GameObject healthParent;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private ParticleSystem eatMotion;
    [SerializeField] private GameObject _nurfParticle;

    [Header("Data")]
    public CustomerType customerType;
    [SerializeField] private CustomerDebuff debuff;

    public Animator animator;
    public GameObject avatar;

    private float difficultyMultiplier;

    private float maxHp;
    public float CurrentHP { get; private set; }

    public Transform CurrentRunTarget { get; private set; }
    public Transform CurrentHitTarget { get; private set; }

    private Color originalColor;

    private bool isCleared = false;

    public float FinalSpeed =>
        customerType.customerSpeed
        * GlobalEnemyModifier.Instance.GlobalSpeedMultiplier
        * debuff.SpeedMultiplier;

    public int FinalDamage =>
        Mathf.RoundToInt(
            customerType.customerDamage
            * difficultyMultiplier
            * GlobalEnemyModifier.Instance.GlobalDamageMultiplier
            * debuff.DamageMultiplier
        );

    private void Awake()
    {
        difficultyMultiplier = CustomerSpawner.Instance._difficultyMultiplier;

        RunState = new RunState(this);
        AttackState = new AttackState(this);
        CloseState = new CloseState(this);
        ClearState = new ClearState(this);
        DeadState = new DeadState(this);

        originalColor = sr.color;

        debuff.OnChanged += OnStatChanged;
        GlobalEnemyModifier.Instance.OnChanged += OnStatChanged;
    }

    private void OnDestroy()
    {
        _onGameOver.OnEventRaised -= HandleClearRequested;
        OnClearRequested -= HandleClearRequested;
        debuff.OnChanged -= OnStatChanged;
        GlobalEnemyModifier.Instance.OnChanged -= OnStatChanged;
    }

    private void OnEnable()
    {
        OnClearRequested += HandleClearRequested;
        _onGameOver.OnEventRaised += HandleClearRequested;

        runTargets = CustomerSpawner.Instance.runTargets;
        hitTargets = CustomerSpawner.Instance.heatTargets;

        InitializeStats();
        PickRandomTargets();
    }

    private void OnDisable()
    {
        OnClearRequested -= HandleClearRequested;
        _onGameOver.OnEventRaised -= HandleClearRequested;
    }
    private void Start()
    {
        ChangeState(RunState);
        damageText.DOFade(0, 0);
    }

    private void Update()
    {
        UpdateHPUI();
        currentState?.Update();

       
    }

    private void InitializeStats()
    {
        maxHp = Mathf.RoundToInt(customerType.customerHP * difficultyMultiplier);
        CurrentHP = maxHp;
        UpdateHPUI();
    }

    private void OnStatChanged()
    {
        Instantiate(_nurfParticle, transform.position, Quaternion.identity);
    }

    private void UpdateHPUI()
    {
        float ratio = CurrentHP / maxHp;
        healthParent.transform.localScale = new Vector3(ratio, 1f, 1f);
        hpText.text = $"{CurrentHP}/{maxHp}";
    }

    public void PickRandomTargets()
    {
        if (runTargets == null || runTargets.Length == 0) return;
        if (hitTargets == null || hitTargets.Length == 0) return;

        CurrentRunTarget =
            runTargets[UnityEngine.Random.Range(0, runTargets.Length)];

        CurrentHitTarget =
            hitTargets[UnityEngine.Random.Range(0, hitTargets.Length)];
    }

    public void ChangeState(IEnemyState newState)
    {
        if (isCleared) return;

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();

        if (newState == ClearState)
            isCleared = true;
    }

    public Collider2D IsAttackTargetInRange()
    {
        return Physics2D.OverlapCircle(
            transform.position,
            attackRange.x,
            truckMask
        );
    }

    public Collider2D IsCloseTargetInRange()
    {
        return Physics2D.OverlapCircle(
            transform.position,
            closeRange.x,
            closeMask
        );
    }

    public void TakeDamage(int damage)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => damageText.text = "-" + damage);
        seq.Append(damageText.DOFade(1, 0));
        seq.AppendInterval(0.75f);
        seq.Append(damageText.DOFade(0, 0.75f));

        CurrentHP = Mathf.Max(CurrentHP - damage, 0);
        StartCoroutine(HitColorEffect());

        if (CurrentHP <= 0)
        {
            ChangeState(DeadState);
            ItemManager.Instance.TryItemDrop(gameObject.transform);
        }
    }

    private IEnumerator HitColorEffect()
    {
        sr.color = Color.red;
        eatMotion.Play();

        yield return new WaitForSeconds(0.1f);

        float t = 0f;
        float duration = 0.15f;

        while (t < duration)
        {
            t += Time.deltaTime;
            sr.color = Color.Lerp(Color.red, originalColor, t / duration);
            yield return null;
        }

        sr.color = originalColor;
    }

    public void InflictDamage()
    {
        TruckHealthManager.Instance.TruckHit(FinalDamage);
    }

    public void RequestClear()
    {
        OnClearRequested?.Invoke();
    }

    public void HandleClearRequested()
    {
        ChangeState(ClearState);
    }

    public void PlayHitParticle()
    {
        if (CurrentHitTarget == null) return;
        hitParticle.PlayAt(CurrentHitTarget.parent.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange.x);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, closeRange.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}
