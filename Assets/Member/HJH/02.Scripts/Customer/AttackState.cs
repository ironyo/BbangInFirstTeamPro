using System.Collections;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Customer customer;

    public AttackState(Customer customer)
    {
        this.customer = customer;
    }

    public void Enter()
    {
        Debug.Log("Enemy¿¡¼­ Attack");
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
