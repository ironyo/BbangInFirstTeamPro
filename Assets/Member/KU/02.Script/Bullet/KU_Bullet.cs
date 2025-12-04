using UnityEngine;

public abstract class KU_Bullet : MonoBehaviour
{
    public Rigidbody2D _rigidbodyCompo { get; private set; }

    private Vector2 _moveDir;
    public float _moveSpeed { get; private set; } = 5;

    private Vector3 rotationSpeed = new Vector3(0, 0, 10);

    public KU_Enemy targetEnemy { get; private set; }

    protected virtual void Awake()
    {
        _rigidbodyCompo = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        transform.rotation *= Quaternion.Euler(new Vector3(0, 0, Random.Range(-30f, 30f)));
    }
    public virtual void Update()
    {
        transform.rotation *= Quaternion.Euler(rotationSpeed);
    }

    public void GetTarget(KU_Enemy pos)
    {
        targetEnemy = pos;
        _moveDir = pos.gameObject.transform.position - transform.position;
        _rigidbodyCompo.linearVelocity = _moveDir.normalized * _moveSpeed;
    }

    public void StopBullet()
    {
        rotationSpeed = Vector3.zero;
        _moveSpeed = 0;
        _rigidbodyCompo.linearVelocity = Vector3.zero;
    }
}
