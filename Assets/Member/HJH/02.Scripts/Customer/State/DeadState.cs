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
        customer.tag = "DeadCustomer";
        customer.gameObject.layer = LayerMask.NameToLayer("DeadCustomer");
    }

    public void Exit()
    {

    }

    public void Update()
    {
        rb.linearVelocity = new Vector2(-3.5f,0);
    }
}
