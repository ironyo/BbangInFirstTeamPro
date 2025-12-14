using UnityEngine;

public class CloseState : IEnemyState
{
    private readonly Customer customer;
    private readonly Rigidbody2D rb;
    private readonly Animator animator;

    private Vector2 currentDir;
    private float randomOffset;

    public CloseState(Customer customer)
    {
        this.customer = customer;
        rb = customer.GetComponent<Rigidbody2D>();
        animator = customer.animator;
    }

    public void Enter()
    {
        animator.SetBool("OnWalk", true);

        randomOffset = Random.Range(-1.25f, 1.25f);

        Transform target = customer.CurrentHitTarget;
        if (target == null) return;

        currentDir =
            (target.position - customer.transform.position).normalized;
    }

    public void Update()
    {
        Transform target = customer.CurrentHitTarget;
        if (target == null) return;

        if (customer.IsAttackTargetInRange())
        {
            customer.ChangeState(customer.AttackState);
            return;
        }

        Vector2 targetDir =
            ((Vector2)target.position
            + new Vector2(randomOffset, 0f)
            - (Vector2)customer.transform.position).normalized;

        currentDir = Vector2.Lerp(
            currentDir,
            targetDir,
            Time.deltaTime * 5f
        );

        rb.linearVelocity = currentDir * customer.FinalSpeed;
    }

    public void Exit()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("OnWalk", false);
    }
}
