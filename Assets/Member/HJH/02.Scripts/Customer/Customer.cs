using NUnit.Framework.Interfaces;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
    [Header("Gizmo Range")]
    [SerializeField] private Vector2 closeRange;
    [SerializeField] private Vector2 attackRange;

    [Header("Run Targets")]
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
    [SerializeField] private ParticleSystem deadMotion;

    [SerializeField] private CustomerTypeList customerTypeList;

    public Animator _animator;

    [SerializeField]private CustomerType customerType;
    public int customerHP { get; set; }
    private int maxHp => customerType.customerHP;

    public float customerSpeed { get; set; }

    [SerializeField]private SpriteRenderer sr;
    private Color originalColor; 

    private void Awake()
    {
        customerHP = customerType.customerHP;
        customerSpeed = customerType.customerSpeed;

        RunState = new RunState(this);
        AttackState = new AttackState(this);
        ClearState = new ClearState(this);
        CloseState = new CloseState(this);
        DeadState = new DeadState(this);

        originalColor = sr.color;
    }
    private bool isCleared = false;

    private void OnEnable()
    {
        runTargets = CustomerSpawner.Instance.runTargets;
        hitTagets = CustomerSpawner.Instance.heatTargets;
    }
    private void Start()
    {
        ChangeState(RunState);
    }

    private void Update()
    {
        customerHP = Mathf.Clamp(customerHP, 0, maxHp);

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
}