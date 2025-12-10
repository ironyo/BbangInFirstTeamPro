using DG.Tweening;
using UnityEngine;

public class ClearState : IEnemyState
{
    private Customer customer;
    private Transform customerTransform;
    private Rigidbody2D rb;
    public ClearState(Customer enemy)
    {
        this.customer = enemy;
        customerTransform = enemy.transform;
        rb = this.customer.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {
        customerTransform.DOFlip();
    }

    public void Update()
    { 
        rb.linearVelocity = new Vector2(-8,0);
    }
    public void Exit() { }
}
