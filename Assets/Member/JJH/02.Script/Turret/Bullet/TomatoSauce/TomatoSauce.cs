using UnityEngine;

public class TomatoSauce : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Vector3 originalScale;
    private float baseLength;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        //스프라이트가 화면에서 차지하는 가로 크기
        baseLength = sprite.size.x;
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = originalScale;
    }

    public void ShotTomatoSauce(Transform target, Transform origin, int damage)
    {
        Vector3 dir = (target.position - origin.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 scale = originalScale;
        scale.x = 20f / baseLength;
        transform.localScale = scale;

        transform.position = origin.position - transform.right * 0.85f;

        if (TryGetComponent<LaserFade>(out var laser))
        {
            laser.enabled = true;
            laser.damage = damage;
        }
    }
}
