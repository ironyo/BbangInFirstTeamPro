using UnityEngine;

public class DeadState : IEnemyState
{
    private readonly Customer customer;
    private readonly Rigidbody2D rb;
    private readonly Animator animator;

    private readonly int rewardMoney;

    public DeadState(Customer customer)
    {
        this.customer = customer;
        rb = customer.GetComponent<Rigidbody2D>();
        animator = customer.animator;
        rewardMoney = customer.customerType.money;
    }

    public void Enter()
    {
        rb.linearVelocity = Vector2.zero;

        customer.avatar.transform.rotation = Quaternion.identity;

        animator.SetBool("isDead", true);

        customer.tag = "DeadCustomer";
        customer.gameObject.layer =
            LayerMask.NameToLayer("DeadCustomer");
        MoneyManager.Instance.AddMoney(rewardMoney);
    }

    public void Update()
    {
        rb.linearVelocity = new Vector2(-4.5f, 0f);
    }

    public void Exit() { }
}
