using UnityEngine;

public class RunState : IEnemyState
{
    private readonly Customer customer;
    private readonly Rigidbody2D rb;
    private readonly Animator animator;

    public RunState(Customer customer)
    {
        this.customer = customer;
        rb = customer.GetComponent<Rigidbody2D>();
        animator = customer.animator;
    }

    public void Enter()
    {
        animator.SetBool("OnWalk", true);
    }

    public void Update()
    {
        Transform target = customer.CurrentRunTarget;
        if (target == null) return;

        Vector2 dir =
            (target.position - customer.transform.position).normalized;

        rb.linearVelocity = dir * customer.FinalSpeed;

        if (customer.IsCloseTargetInRange())
        {
            customer.ChangeState(customer.CloseState);
        }
    }

    public void Exit()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("OnWalk", false);
    }
}
