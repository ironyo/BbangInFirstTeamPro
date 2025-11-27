using UnityEngine;

public class KU_Bullet : MonoBehaviour
{
    private BoxCollider2D _colliderCompo;
    private Rigidbody2D _rigidbodyCompo;

    private Vector2 _moveDir;
    [SerializeField] private float _moveSpeed;

    private Vector3 rotationSpeed = new Vector3(0, 0, 10);

    private void Awake()
    {
        _colliderCompo = GetComponent<BoxCollider2D>();
        _rigidbodyCompo = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbodyCompo.linearVelocity += _moveDir.normalized * Time.deltaTime * _moveSpeed;
        transform.rotation *= Quaternion.Euler(rotationSpeed);
    }   
    public void GetTarget(Transform pos)
    {
        _moveDir = pos.position - transform.position;
    }
}