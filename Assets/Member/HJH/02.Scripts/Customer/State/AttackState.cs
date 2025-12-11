using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Customer customer;
    private Rigidbody2D rb;
    private Animator animator;

    private Transform avatar;

    private bool isAttacking = false;

    public AttackState(Customer customer)
    {
        this.customer = customer;
        animator = customer._animator;
    }

    public void Enter()
    {
        avatar = customer.avatar.transform;

        rb = customer.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        LookAtClosestTarget();
    }

    public void Update()
    {
        if (customer.customerHP <= 0)
        {
            animator.SetBool("isAttack", false);
            customer.ChangeState(customer.ClearState);
            return;
        }

        if (customer.IsAttackTargetInRange() == false)
        {
            animator.SetBool("isAttack", false);
            customer.ChangeState(customer.CloseState);
            return;
        }

        if(customer.IsAttackTargetInRange() == true)
        {
            AttackTween();
        }
    }

    public void Exit()
    {

    }
    private void AttackTween()
    {
        if (isAttacking) return;
        isAttacking = true;

        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => animator.SetBool("isAttack", true));
        seq.AppendInterval(0.75f);

        seq.AppendCallback(() =>
        {
            isAttacking = false;
        });
    }

    private Transform GetClosestTarget()
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (var t in customer.runTargets)
        {
            float dist = Vector2.Distance(customer.transform.position, t.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = t;
            }
        }

        return closest;
    }

    private void LookAtClosestTarget()
    {
        Transform target = GetClosestTarget();
        if (target == null) return;

        Transform pivot = avatar;

        Vector2 dir = target.position - pivot.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (target.position.y < 0)
        {
            angle += 180;
            Debug.Log("angle+:" + angle);
        }
        else
        {
            angle -= 180;
            Debug.Log("angle-:" + angle);
        }

            Quaternion targetRot = Quaternion.Euler(0, 0, angle);

        pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRot, 0.3f);
    }

    private void BackMotion()
    {
        float y = avatar.position.y;
        Vector3 offset = (y < 0f)
            ? new Vector3(-10f, -3f, 0f)
            : new Vector3(-10f, 3f, 0f);

        customer.StartCoroutine(BackOffRoutine(offset, 0.5f));
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

        // 회전 초기화
        avatar.rotation = Quaternion.identity;

    }

}
