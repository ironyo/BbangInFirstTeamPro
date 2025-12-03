using System.Collections;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Customer customer;
    private Rigidbody2D rb;

    public AttackState(Customer customer)
    {
        this.customer = customer;
    }

    public void Enter()
    {
        Debug.Log("Enemy¿¡¼­ Attack");
        rb = customer.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }

    public void Update() 
    {
        if(customer.IsAttackTargetInRange() == false)
        {
            customer.ChangeState(customer.CloseState);
        }
        if(customer.customerHP <= 0)
        {
            customer.ChangeState(customer.ClearState);
        }
    }
    public void Exit() { }
}
