using UnityEngine;

public class CloseState : IEnemyState
{
    private Customer customer;
    private Transform target;

    private Vector2 currentDir;

    private float moveSpeed;
    private float maxSpeed;
    private float accelRate = 1.5f;

    private Rigidbody2D rb;
    private float random;

    private Animator animator;

    public CloseState(Customer customer)
    {
        this.customer = customer;
        rb = customer.GetComponent<Rigidbody2D>();
        animator = customer._animator;
    }

    public void Enter()
    {
        animator.SetBool("OnWalk", true);
        Debug.Log("Customer CloseState Enter");

        moveSpeed = customer.customerSpeed - 2;
        maxSpeed = customer.customerSpeed * 0.4f;

        target = GetClosestTarget();

        if (target != null)
            currentDir = (target.position - customer.transform.position).normalized;

        random = Random.Range(-1.25f, 1.25f);
    }

    public void Update()
    {
        if (target == null) return;

        if (customer.IsAttackTargetInRange())
        {
            customer.ChangeState(customer.AttackState);
            return;
        }

        if (customer.isSlow)
        {
            float minSpeed = customer.customerSpeed - 4f;
            float max = maxSpeed;

            moveSpeed = Mathf.Clamp(moveSpeed - 2f, minSpeed, max);

            Debug.Log($"moveSpeed: {moveSpeed}");
        }

        Vector2 targetDir =
                ((Vector2)target.position + new Vector2(random, 0f) -
                 (Vector2)customer.transform.position).normalized;

        currentDir = Vector2.Lerp(currentDir, targetDir, Time.deltaTime * moveSpeed);

        moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed, accelRate * Time.deltaTime);

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

    public void Exit()
    {
        animator.SetBool("OnWalk", false);
    }
}
