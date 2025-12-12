using DG.Tweening;
using UnityEngine;

public class ClearState : IEnemyState
{
    private Customer customer;
    private Transform customerTransform;
    private Rigidbody2D rb;

    private Animator animator;
    public ClearState(Customer enemy)
    {
        this.customer = enemy;
        customerTransform = enemy.transform;
        rb = this.customer.GetComponent<Rigidbody2D>();
        animator = customer._animator;
    }

    public void Enter()
    {
        customer.gameObject.layer = LayerMask.NameToLayer("DeadCustomer");
        customer.avatar.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        animator.SetBool("isAttack", false);
        animator.SetBool("isHeat", false);
    }

    public void Update()
    { 
        rb.linearVelocity = new Vector2(-8,0);
    }
    public void Exit() { }
}
