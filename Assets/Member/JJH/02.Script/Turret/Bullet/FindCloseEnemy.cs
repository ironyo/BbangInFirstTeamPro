using UnityEngine;

public abstract class FindCloseEnemy : MonoBehaviour
{
    protected Transform FindCloseEnemyTrans()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        if (targets.Length == 0)
            return null;

        Transform target = null;
        float closeDistance = Mathf.Infinity;

        foreach (GameObject enemy in targets)
        {
            Vector3 dir = enemy.transform.position - transform.position;
            float dirSquare = dir.sqrMagnitude;

            if (dirSquare < closeDistance)
            {
                closeDistance = dirSquare;
                target = enemy.transform;
            }
        }

        return target;
    }
}
