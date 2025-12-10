using System;
using Assets.Member.CHG._02.Scripts.Bullet;
using Assets.Member.CHG._02.Scripts.Pooling;
using UnityEngine;

public class ProjectileCurve : ProjectileBase, IRecycleObject
{
    private Vector2 _start;
    private Vector2 _end;
    private Vector2 _point;
    private float _duration;
    private float _t;
    private bool _isLaunched = false;
    private bool _isHit = false;
    private const float defaultAngle = 35f;
    public float Angle = defaultAngle;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float _startHigh = 0.4f;
    [SerializeField] private float _backHigh = 0.9f;
    [SerializeField] private float _returnPower = 1.3f;
    [SerializeField] private GameObject HitParticle;
    public Action<IRecycleObject> Destroyed { get; set; }
    GameObject IRecycleObject.GameObject => base.gameObject;

    public override void SetUp(Transform shooter, Transform target)
    {
        ResetState(shooter);

        base.SetUp(shooter, target);

        _start = shooter.position;
        _end = target.position;

        float distance = Vector3.Distance(_start, _end);
        _duration = distance / _movementRigidBody.MoveSpeed;

        float shootAngle = Angle + Utils.GetAngleFromPosition(_start, _end);
        _point = Utils.GetNewPoint(_start, shootAngle, distance * _startHigh);

        _t = 0;

        _isLaunched = true;
    }

    private void ResetState(Transform shooter)
    {
        transform.position = shooter.position;
        _isLaunched = false;
        _isHit = false;
        _t = 0;
    }

    private void Update()
    {
        if (_isLaunched)
            Process();
    }

    public override void Process()
    {
        _t += Time.deltaTime / _duration;

        IsArrivedToTarget();
        if (!_isHit)
        {
            transform.position = Utils.QuadraticCurve(_start, _point, _end, _t);
        }
        else if (_isHit)
        {
            transform.position = Utils.QuadraticCurve(_end, _point, _start, _t);
        }

        transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);

    }

    private void IsArrivedToTarget()
    {
        float distance = Vector3.Distance(transform.position, _end);

        if (distance < 0.1f)
        {
            ProjectileReturn();
        }
    }

    protected override void OnHit(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector3 particleDirection = collision.transform.position - transform.position;
            float ParticleAngle = Mathf.Atan2(particleDirection.y, particleDirection.x) * Mathf.Rad2Deg;
            Instantiate(HitParticle, collision.transform.position, Quaternion.Euler(0, 0, ParticleAngle));

            //데미지 적용

            CameraShake.Instance.ImpulseForce(0.03f);

            if (_isHit) return;
            ProjectileReturn();
        }
        else if (collision.CompareTag("Player"))
        {
            if (_isHit)
            {
                Destroyed?.Invoke(this);
            }
        }
    }

    private void ProjectileReturn()
    {
        if (_isHit) return;
        float distance = Vector3.Distance(_end, _start) * _returnPower;
        _duration = distance / _movementRigidBody.MoveSpeed;

        float reverseAngle = -Angle;

        float baseAngle = Utils.GetAngleFromPosition(_end, _start);

        float shootAngle = reverseAngle + baseAngle;

        _point = Utils.GetNewPoint(_end, shootAngle, -distance * _backHigh);

        _t = 0;
        _isHit = true;
    }
}
