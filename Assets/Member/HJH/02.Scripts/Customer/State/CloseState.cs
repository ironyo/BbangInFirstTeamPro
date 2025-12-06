using UnityEngine;

public class CloseState : IEnemyState
{
    private Customer customer;
    private Transform target;

    private Vector2 currentDir;

    private float moveSpeed;
    private float maxSpeed = 5.5f;
    private float accelRate = 1.5f;

    private Rigidbody2D rb;
    private float random;

    private Animator animator;

    public CloseState(Customer customer)
    {
        animator = customer._animator;
        this.customer = customer;
        rb = customer.GetComponent<Rigidbody2D>();
        moveSpeed = customer.customerSpeed - 2;
    }

    public void Enter()
    {
        animator.SetBool("OnWalk", true);
        Debug.Log("Customer CloseState Enter");
        target = GetClosestTarget();

        if (target != null)
            currentDir = (target.position - customer.transform.position).normalized;

        random = Random.Range(-1.25f, 1.25f); // 오프셋 감소
    }

    public void Update()
    {
        if (target == null) return;

        if (customer.IsAttackTargetInRange())
        {
            customer.ChangeState(customer.AttackState);
            return;
        }

        Vector2 targetDir =
            ((Vector2)target.position + new Vector2(random, 0f) -
            (Vector2)customer.transform.position).normalized;

        // Lerp
        currentDir = Vector2.Lerp(currentDir, targetDir, Time.deltaTime * moveSpeed);

        // 가속
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed, accelRate * Time.deltaTime);

        // 최종 이동
        rb.linearVelocity = currentDir.normalized * moveSpeed;
    }

    private Transform GetClosestTarget()
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (Transform t in customer.hitTagets)
        {
            float dist = Vector2.Distance(customer.transform.position, t.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = t;
            }
        }
        return closest;
    }

    public void Exit() { }
}
