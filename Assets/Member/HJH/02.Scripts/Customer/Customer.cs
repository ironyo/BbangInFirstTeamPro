using DG.Tweening;
using NUnit.Framework.Interfaces;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}
public class Customer : MonoBehaviour
{
    public event System.Action OnClearRequested;

    [Header("Gizmo Range")]
    [SerializeField] private Vector2 closeRange;
    [SerializeField] private Vector2 attackRange;

    [SerializeField] private CustomerHitParticle hitParticle;
    
    public Transform[] runTargets;
    public Transform[] hitTagets;

    [SerializeField] private LayerMask truckMask;
    [SerializeField] private LayerMask closeMask;

    private IEnemyState currentState;

    public RunState RunState { get; set; } // 달려가는 기본 추격
    public AttackState AttackState { get; set; } // 닿았을 떄, 공격
    public ClearState ClearState{ get; set; } // 적의 포 만감을 다 채웠을 때, 자동으로 떠남
    public CloseState CloseState { get; set; } // 가까이 왔을 때, 트럭으로 이동
    public DeadState DeadState { get; set; } // 죽었을 때
    
    [SerializeField] private TextMeshPro hpText;
    [SerializeField] private TextMeshPro damageText;
    [SerializeField] private ParticleSystem deadMotion;
    [SerializeField] private ParticleSystem eatMotion;

    [SerializeField] private CustomerTypeList customerTypeList;

    public Animator _animator;

    public CustomerType customerType;
    public float customerHP { get; set; }
    private float maxHp => customerType.customerHP;
    
    public float customerSpeed { get; set; }

    [SerializeField]private SpriteRenderer sr;
    private Color originalColor;

    public GameObject avatar;

    public GameObject healthParent;

    private bool isCleared = false;

    public event System.Action<bool> OnSlowChanged;

    private bool _isSlow;
    public bool isSlow
    {
        get => _isSlow;
        private set
        {
            if (_isSlow == value) return;
            _isSlow = value;
            OnSlowChanged?.Invoke(_isSlow);
        }
    }

    public int damage { get; set; }

    private void Awake()
    {
        damage = customerType.customerDamage;
        customerHP = customerType.customerHP;
        customerSpeed = customerType.customerSpeed;

        RunState = new RunState(this);
        AttackState = new AttackState(this);
        ClearState = new ClearState(this);
        CloseState = new CloseState(this);
        DeadState = new DeadState(this);

        originalColor = sr.color;
    }
    private void OnEnable()
    {
        runTargets = CustomerSpawner.Instance.runTargets;
        hitTagets = CustomerSpawner.Instance.heatTargets;
    }

    private void Start()
    {

        ChangeState(RunState);
        damageText.DOFade(0, 0);
    }

    private void Update()
    {

        customerHP = Mathf.Clamp(customerHP, 0, maxHp);
        healthParent.transform.localScale = new Vector3(customerHP / maxHp, 1 , 1);
        hpText.text = $"{customerHP.ToString()}/{maxHp}";
        currentState?.Update();
        IsAttackTargetInRange();
        IsCloseTargetInRange();

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            TakeDamage(1);
        }

        /*if (도착함)
        {
            ChangeState(ClearState);
        }*/
    }

    public void ChangeState(IEnemyState newState)
    {
        if (isCleared) return;
        Debug.Log(newState.ToString());

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();

        if (newState == ClearState)
            isCleared = true;
    }

    public Collider2D IsAttackTargetInRange()
    {
        return Physics2D.OverlapCircle(transform.position, attackRange.x, truckMask);
    }

    public Collider2D IsCloseTargetInRange()
    {
        return Physics2D.OverlapCircle(transform.position, closeRange.x, closeMask);
    }

    // 총 맞으면 이거 사용해
    public void TakeDamage(int damage)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(()=>damageText.text = "-" + damage.ToString());
        seq.Append(damageText.DOFade(1, 0));
        seq.AppendInterval(0.75f);
        //seq.JoinCallback(damageText.transform.DOMove());
        seq.Append(damageText.DOFade(0, 0.75f));
        customerHP -= damage;

        StartCoroutine(HitColorEffect());

        if (customerHP <= 0)
        {
            ChangeState(DeadState);
            deadMotion.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheesePuddle"))
        {
            isSlow = true;
        }
        else
        {
            isSlow = false;
        }

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange.x);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,closeRange.x);
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
        Transform closest = GetClosestTarget();
        if (closest == null)
        {
            Debug.LogWarning("Closest target not found");
            return;
        }

        Vector3 spawnPos = closest.parent.position;
        hitParticle.PlayAt(spawnPos);
    }

    public Transform GetClosestTarget()
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (var t in runTargets)
        {
            float dist = Vector2.Distance(transform.position, t.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = t;
            }
        }

        return closest;
    }

    public void InflictDamage()
    {
        TruckHealthManager.Instance.TruckHit(damage);
    }
    private void HandleSlow(bool isSlow)
    {
        if (isSlow)
        {
            Debug.Log("느려짐");
        }
        else
        {
            Debug.Log("원래 속도로 돌아옴");
        }
    }
}