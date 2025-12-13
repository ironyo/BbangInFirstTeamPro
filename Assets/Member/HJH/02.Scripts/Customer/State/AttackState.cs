using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Customer customer;
    private Rigidbody2D rb;
    private Animator animator;

    private Transform avatar;

    private float attackInterval;

    private CancellationTokenSource attackCTS;

    public AttackState(Customer customer)
    {
        this.customer = customer;
        animator = customer._animator;
        attackInterval = customer.customerType.attackInterval;
    }

    public void Enter()
    {
        avatar = customer.avatar.transform;

        rb = customer.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        LookAtClosestTarget();

        attackCTS = new CancellationTokenSource();
        AttackLoopAsync(attackCTS.Token).Forget();
    }

    public void Update()
    {
        if (customer.customerHP <= 0)
        {
            customer.ChangeState(customer.ClearState);
            return;
        }

        if (!customer.IsAttackTargetInRange())
        {
            customer.ChangeState(customer.CloseState);
            return;
        }
    }

    public void Exit()
    {
        attackCTS?.Cancel();
        attackCTS?.Dispose();
        attackCTS = null;

        animator.SetBool("isAttack", false);
    }

    private async UniTaskVoid AttackLoopAsync(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                if (!customer.IsAttackTargetInRange() || customer.customerHP <= 0)
                {
                    animator.SetBool("isAttack", false);
                    return;
                }

                animator.SetBool("isAttack", true);

                await UniTask.Delay(
                    TimeSpan.FromSeconds(attackInterval/3),
                    cancellationToken: token
                );

                customer.transform.rotation = Quaternion.identity;
                animator.SetBool("isAttack", false);

                await UniTask.Delay(
                    TimeSpan.FromSeconds(attackInterval),
                    cancellationToken: token
                );
            }
        }
        catch (OperationCanceledException)
        {
            customer.ChangeState(customer.CloseState);
        }
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

        Vector2 dir = target.position - avatar.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (dir.y < 0)
            angle += 180f;
        else
            angle -= 180f;

        Quaternion targetRot = Quaternion.Euler(0, 0, angle);
        avatar.rotation = targetRot;
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

        avatar.rotation = Quaternion.identity;
    }
}
