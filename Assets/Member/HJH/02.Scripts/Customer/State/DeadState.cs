using UnityEngine;

public class DeadState : IEnemyState
{
    private Customer customer;
    private Rigidbody2D rb;

    private Animator animator;
    public DeadState(Customer customer)
    {
        animator = customer._animator;
        this.customer = customer;
        rb = customer.GetComponent<Rigidbody2D>();
    }
    public void Enter()
    {
        animator.SetBool("isDead", true);
    }

    public void Exit()
    {

    }

    public void Update()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
