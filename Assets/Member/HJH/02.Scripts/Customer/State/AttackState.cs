using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class AttackState : IEnemyState
{
    private readonly Customer customer;
    private readonly Animator animator;
    private Transform avatar;

    private CancellationTokenSource cts;

    public AttackState(Customer customer)
    {
        this.customer = customer;
        animator = customer.animator;
    }

    public void Enter()
    {
        avatar = customer.avatar.transform;

        var rb = customer.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        cts = new CancellationTokenSource();
        AttackLoopAsync(cts.Token).Forget();
    }

    public void Update()
    {
        if (customer.CurrentHP <= 0)
        {
            customer.ChangeState(customer.DeadState);
            return;
        }

        if (!customer.IsAttackTargetInRange())
        {
            customer.ChangeState(customer.CloseState);
        }
    }

    public void Exit()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;

        animator.SetBool("isAttack", false);
        avatar.rotation = Quaternion.identity;
    }

    private async UniTaskVoid AttackLoopAsync(CancellationToken token)
    {
        float animTime = customer.customerType.attackInterval;

        try
        {
            while (!token.IsCancellationRequested)
            {
                LookAtTarget();

                animator.SetBool("isAttack", true);

                await UniTask.Delay(
                    TimeSpan.FromSeconds(animTime),
                    cancellationToken: token
                );

                animator.SetBool("isAttack", false);
                avatar.rotation = Quaternion.identity;

                await UniTask.Delay(
                    TimeSpan.FromSeconds(animTime * 3f),
                    cancellationToken: token
                );
            }
        }
        catch (OperationCanceledException)
        {
            // 정상 종료
        }
    }

    private void LookAtTarget()
    {
        Transform target = customer.CurrentHitTarget;
        if (target == null) return;

        Vector2 dir = target.position - avatar.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        avatar.rotation = Quaternion.Euler(0, 0, angle);
    }
}
