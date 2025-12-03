using NUnit.Framework.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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

    private IEnemyState currentState;

    public RunState RunState { get; set; } // 달려가는 기본 추격
    public AttackState AttackState { get; set; } // 닿았을 떄, 공격
    public ClearState ClearState{ get; set; } // 적의 포만감을 다 채웠을 때, 자동으로 떠남
    public CloseState CloseState { get; set; } // 가까이 왔을 때, 트럭으로 이동

    [SerializeField] private CustomerType customerType;
    [SerializeField] private TextMeshPro hpText;
    [SerializeField] private ParticleSystem deadMotion;

    public int customerHP { get; set; }
    private int maxHp => customerType.customerHP;

    private float customerSpeed;
    private float customerAttackSpeed;

    private void Awake()
    {
        runTargets = CustomerSpawner.Instance.runTargets;
        hitTagets = CustomerSpawner.Instance.heatTargets;

        customerHP = customerType.customerHP;
        customerSpeed = customerType.customerSpeed;
        customerAttackSpeed = customerType.customerAttackSpeed;

        RunState = new RunState(this);
        AttackState = new AttackState(this);
        ClearState = new ClearState(this);
        CloseState = new CloseState(this);
    }
    private bool isCleared = false;

    private void Start()
    {
        ChangeState(RunState);
    }

    private void Update()
    {
        hpText.text = $"{customerHP.ToString()}/{maxHp}";
        currentState?.Update();
        IsAttackTargetInRange();
        IsCloseTargetInRange();
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
        return Physics2D.OverlapCircle(transform.position, attackRange.x, truckMask);
    }

    public Collider2D IsCloseTargetInRange()
    {
        return Physics2D.OverlapCircle(transform.position, closeRange.x, truckMask);
    }

    /// 총 맞으면 이거 사용해
    public void TakeDamage(int damage)
    {
        customerHP -= damage;

        if (customerHP <= 0)
        {
            Debug.Log("에너미 죽음");
            deadMotion.Play();
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
}