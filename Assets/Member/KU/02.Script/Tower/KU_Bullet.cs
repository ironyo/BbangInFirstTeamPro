using UnityEngine;

public abstract class KU_Bullet : MonoBehaviour
{
    public Rigidbody2D _rigidbodyCompo;

    public Vector2 _moveDir { get; private set; }
    public float _moveSpeed { get; set; } = 5;

    private Vector3 rotationSpeed = new Vector3(0, 0, 10);

    protected virtual void Awake()
    {
        _rigidbodyCompo = GetComponent<Rigidbody2D>();
    }
    public virtual void Update()
    {
        _rigidbodyCompo.linearVelocity += _moveDir.normalized * Time.deltaTime * _moveSpeed;
        transform.rotation *= Quaternion.Euler(rotationSpeed);
    }

    public void GetTarget(Transform pos)
    {
        _moveDir = pos.position - transform.position;
    }
}
