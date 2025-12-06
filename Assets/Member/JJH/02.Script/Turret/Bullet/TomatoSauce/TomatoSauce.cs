using UnityEngine;

public class TomatoSauce : MonoBehaviour
{
    [SerializeField] private Transform origin;

    private SpriteRenderer sprite;

    private float baseLength;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        //스프라이트가 화면에서 차지하는 가로 크기
        baseLength = sprite.size.x;
    }

    public void ShotTomatoSauce(Transform target)
    {
        Vector3 dir = target.position - origin.position;
        float dist = dir.magnitude; //벡터 크기 구하기

        transform.position = origin.position;

        //적쪽으로 회전
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //스케일 거리만큼 늘리기
        Vector3 scale = transform.localScale;
        scale.x = dist / baseLength;
        transform.localScale = scale;

        GetComponent<LaserFade>().enabled = true;
    }
}
