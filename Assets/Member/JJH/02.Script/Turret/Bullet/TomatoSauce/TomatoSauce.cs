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

    public void ShotTomatoSauce(Transform target, Transform origin)
    {
        Vector3 dir = target.position - origin.position;
        float dist = dir.magnitude; //벡터 크기 구하기

        transform.position = origin.position - transform.up * 0.85f;

        //적쪽으로 회전
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //스케일 거리만큼 늘리기
        Vector3 scale = transform.localScale;
        scale.x = dist / baseLength;
        transform.localScale = scale;

        if (gameObject.TryGetComponent<LaserFade>(out LaserFade laser))
            laser.enabled = true;
    }
}
