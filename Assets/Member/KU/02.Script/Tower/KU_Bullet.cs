using UnityEngine;

public class KU_Bullet : MonoBehaviour
{
    private BoxCollider2D _colliderCompo;
    private Rigidbody2D _rigidbodyCompo;

    private Vector2 _moveDir;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _colliderCompo = GetComponent<BoxCollider2D>();
        _rigidbodyCompo = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbodyCompo.linearVelocity += _moveDir * Time.deltaTime * _moveSpeed;
    }
    public void GetTarget(Transform pos)
    {
        _moveDir = pos.position - transform.position;
    }
}