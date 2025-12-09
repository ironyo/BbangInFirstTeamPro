using Assets.Member.CHG._02.Scripts.Bullet;
using UnityEngine;

public class ProjectileBoomerang : ProjectileBase
{
    private Vector2 _start;
    private Vector2 _end;

    [SerializeField] private float ReturnTime;
    private float _duration;
    private float _t;
    private bool _isLaunched = false;
    private bool _isHit = false;
    private float _speed;
    public override void SetUp(Transform shooter ,Transform target)
    {
        base.SetUp(shooter, target);

        _start = transform.position;
        _end = target.position;

        float distance = Vector2.Distance(_start, _end);
        _duration = distance / _movementRigidBody.MoveSpeed;
        _movementRigidBody.MoveTo((_end - _start).normalized);

        _speed = _movementRigidBody.MoveSpeed;
        _isLaunched = true;
    }

    private void Update()
    {
        if (_isLaunched)
            Process();
    }
    public override void Process()
    {
        if (_isHit)
        {
            _t += Time.deltaTime;
            float returnTime = _t / ReturnTime;

            float newSpeed = Mathf.Lerp(_speed, -_speed, returnTime);

            _movementRigidBody.ChangeSpeed(newSpeed);
        }
    }

    protected override void OnHit(Collider2D collision)
    {
        Debug.Log("Hit");
        if (collision.CompareTag("Enemy"))
        {
            _isHit = true;
        }
        else if (collision.CompareTag("Player"))
        {
            if (_isHit)
                Destroy(gameObject);
        }
    }

}
