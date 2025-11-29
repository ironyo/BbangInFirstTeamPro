using Assets.Member.CHG._02.Scripts.Bullet;
using UnityEngine;

public class ProjectileQuadraticHoming : ProjectileBase
{
    private Vector2 _start;
    private Vector2 _end;
    private Vector2 _point;
    private float _duration;
    private float _t;
    private Transform _target;
    private bool _isLaunched = false;
    [SerializeField] private float _arcHeight = 5f;
    [SerializeField] private GameObject BombExplosion;
    [SerializeField] private GameObject HitEffect;

    public override void SetUp(Transform target, float damage, int maxCount = 1, int index = 0)
    {
        base.SetUp(target, damage);

        _target = target;
        _start = transform.position;
        _end = _target.position;

        //시작 지점에서 목표까지에 거리 계산
        float distance = Vector3.Distance(_start, _end);
        //시간 설정 (거리/속도)
        _duration = distance / _movementRigidBody.MoveSpeed;
        if (_duration < 0.5f)
            _duration = 0.5f;

        Vector2 midPoint = Utils.Lerp(_start, _end, 0.5f);

        Vector2 direction = (_end - _start).normalized;

        Vector2 perpendicular = new Vector2(-direction.y, direction.x);

        if (direction.x < 0)
            perpendicular *= -1;

        _point = midPoint + perpendicular * _arcHeight;


        _t = 0;
        _isLaunched = true;

        GameObject bomb = Instantiate(BombExplosion, _target.position, Quaternion.identity);
        bomb.GetComponent<BombExplosion>().SetUp(_duration);
    }

    private void Update()
    {
        if (_isLaunched)
            Process();
    }

    public override void Process()
    {
        _t += Time.deltaTime / _duration;
        transform.position = Utils.QuadraticCurve(_start, _point, _end, _t);
        //transform.localScale = Utils.ScaleChange(_originalScale, MaxScale, _t);

        if (_t > 1)
        {
            OnHit();
            Destroy(gameObject);
        }

    }

    protected override void OnHit()
    {
        if (HitEffect != null)
        {
            Instantiate(HitEffect, _end, Quaternion.identity);
        }
    }
}
