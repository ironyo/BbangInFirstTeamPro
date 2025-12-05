using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Customer customer;
    private Rigidbody2D rb;

    private bool isAttacking = false;

    public AttackState(Customer customer)
    {
        this.customer = customer;
    }

    public void Enter()
    {
        Debug.Log("Enemy에서 Attack");
        rb = customer.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }

    public void Update() 
    {
        if(customer.customerHP <= 0)
        {
            customer.ChangeState(customer.ClearState);
        }

        if(customer.IsAttackTargetInRange() == false)
        {
            customer.ChangeState(customer.CloseState);
        }
        else if(!isAttacking)
        {
            AttackDotween();
        }
    }
    public void Exit() { }

    public void AttackDotween()
    {
        if (isAttacking) return;
        isAttacking = true;
        Sequence tween = DOTween.Sequence();

        tween.AppendCallback(() => Debug.Log("애니메이션"));

        tween.AppendInterval(0.75f);

        tween.AppendCallback(() =>
        {
            float y = customer.transform.position.y;
            Vector3 offset;

            if (y < 0f)
                offset = new Vector3(-10f, -3f, 0f);
            else
                offset = new Vector3(-10f, 3f, 0f);

            customer.StartCoroutine(BackOffRoutine(offset, 0.5f));
        });
    }

    private IEnumerator BackOffRoutine(Vector3 offset, float duration)
    {
        Vector3 start = customer.transform.position;
        Vector3 end = start + offset;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            customer.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
        isAttacking = false;
    }
}
