using UnityEngine;

public class DeadState : IEnemyState
{
    private Customer customer;
    private Rigidbody2D rb;
    public DeadState(Customer customer)
    {
        this.customer = customer;
        customer.GetComponentInChildren<Rigidbody2D>();
    }
    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
