using Assets.Member.CHG._02.Scripts.Bullet;
using UnityEngine;

public class TestShooting : MonoBehaviour
{
    [SerializeField] private float CooldownTime = 2f;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private GameObject ProjectileShadowPrefab;
    [SerializeField] private Transform SkillSpawnPoint;
    [SerializeField] private Transform Target;
    public LayerMask EnemyLayer;
    public float Range;
    private float _currentCoolTime = 0;
    public bool IsSkillAcailable => (Time.time - _currentCoolTime > CooldownTime);
    private void Update()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, Range, EnemyLayer);

        Transform closestTarget = null;
        float shortDistance = Mathf.Infinity;

        foreach (var item in enemys)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance < shortDistance)
            {
                shortDistance = distance;
                closestTarget = item.gameObject.transform;
            }
        }
        Target = closestTarget;

        Vector3 dir = Target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        OnSkill();
    }

    private void OnSkill()
    {
        if (!IsSkillAcailable) return;

        if (ProjectilePrefab != null)
        {
            GameObject projectile = GameObject.Instantiate(ProjectilePrefab, SkillSpawnPoint.position, Quaternion.identity);
            projectile.GetComponent<ProjectileBase>().SetUp(Target, 1);
        }

        if (ProjectileShadowPrefab != null)
        {
            GameObject shadow = GameObject.Instantiate(ProjectileShadowPrefab, SkillSpawnPoint.position, Quaternion.identity);
            shadow.GetComponent<ProjectileBase>().SetUp(Target, 1);
        }

        _currentCoolTime = Time.time;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
