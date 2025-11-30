using UnityEngine;

public class RunState : IEnemyState
{
    private Customer customer;
    private Transform target;

    private Vector2 currentDir;
    private float turnSpeed = 6f;

    public RunState(Customer enemy)
    {
        this.customer = enemy;
    }

    public void Enter()
    {
        target = GetClosestTarget();
        currentDir = customer.transform.right;
    }

    public void Update()
    {
        if (target == null) return;

        if (customer.IsCloseTargetInRange())
        {
            customer.ChangeState(customer.CloseState);
            return;
        }

        Vector2 targetDir = (target.position - customer.transform.position).normalized;

        currentDir = Vector2.Lerp(currentDir, targetDir, Time.deltaTime * turnSpeed).normalized;

        float speed = 3f;
        customer.transform.position += (Vector3)(currentDir * speed * Time.deltaTime);
    }

    public void Exit() { }

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
}
