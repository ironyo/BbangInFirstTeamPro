using Assets.Member.CHG._02.Scripts.Bullet;
using Assets.Member.CHG._02.Scripts.Pooling;
using System;
using UnityEngine;

public class ProjectileBoomerang : ProjectileBase, IRecycleObject
{
    private Vector2 _start;
    private Vector2 _end;

    [SerializeField] private float ReturnTime;
    [SerializeField] private float RotateSpeed = 360f;
    [SerializeField] private float MoveSpeed = 6f;
    [SerializeField] private GameObject HitParticle;
    private float _t;
    private bool _isLaunched = false;
    private bool _isHit = false;
    private float _speed;
    public Action<IRecycleObject> Destroyed { get; set; }
    private Factory _particlefaFactory;
    public GameObject GameObject => gameObject;
    private void Start()
    {
        _particlefaFactory = new Factory(HitParticle, 2);
    }
    public override void SetUp(Transform shooter, Transform target)
    {
        ResetState(shooter);

        base.SetUp(shooter, target);

        _start = transform.position;
        _end = target.position;

        float distance = Vector2.Distance(_start, _end);
        _movementRigidBody.MoveTo((_end - _start).normalized);

        _speed = _movementRigidBody.MoveSpeed;
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
        IsArrivedToTarget();
        if (_isHit)
        {
            _t += Time.deltaTime;
            float returnTime = _t / ReturnTime;

            float newSpeed = Mathf.Lerp(_speed, -_speed, returnTime);

            _movementRigidBody.ChangeSpeed(newSpeed);
        }

        transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);
    }
    private void IsArrivedToTarget()
    {
        float distance = Vector3.Distance(transform.position, _end);

        if (distance < 0.1f)
        {
            _isHit = true;
        }
    }
    protected override void OnHit(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            IRecycleObject particle = _particlefaFactory.Get();
            particle.GameObject.transform.position = collision.gameObject.transform.position;

            CameraShake.Instance.ImpulseForce(0.03f);
        }
        else if (collision.CompareTag("Player"))
        {
            if (_isHit)
            {
                _movementRigidBody.ChangeSpeed(MoveSpeed);
                Destroyed?.Invoke(this);
            }
        }
    }

}
