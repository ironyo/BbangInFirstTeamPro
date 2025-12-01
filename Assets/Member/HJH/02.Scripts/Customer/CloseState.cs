using UnityEngine;

public class CloseState : IEnemyState
{
    private Customer customer;
    private Transform target;

    private Vector2 currentDir;
    private float turnSpeed = 4f;

    private float currentSpeed = 2.5f;   // 시작 속도
    private float maxSpeed = 4.5f;       // 목표 속도
    private float accelRate = 1.5f;      // 가속 속도

    public CloseState(Customer customer)
    {
        this.customer = customer;
    }

    public void Enter()
    {
        target = GetClosestTarget();
        currentDir = customer.transform.right;
        currentSpeed = 2.5f;
    }

    public void Exit() { }

    public void Update()
    {
        if (target == null) return;

        if (customer.IsAttackTargetInRange())
        {
            customer.ChangeState(customer.AttackState);
            return;
        }

        Vector2 targetDir = (target.position - customer.transform.position).normalized;

        // 부드러운 회전
        currentDir = Vector2.Lerp(currentDir, targetDir, Time.deltaTime * turnSpeed).normalized;

        // 가속
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, accelRate * Time.deltaTime);

        customer.transform.position += (Vector3)(currentDir * currentSpeed * Time.deltaTime);
    }

    private Transform GetClosestTarget()
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (var t in customer.heatTagets)
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
}
