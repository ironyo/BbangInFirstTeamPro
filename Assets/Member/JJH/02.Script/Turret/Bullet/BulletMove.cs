using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), (typeof(Collider2D)))]
public class BulletMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed = 5;
    private float nomalSpeed;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private void Awake()
    {
        nomalSpeed = Speed;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Speed = nomalSpeed;
    }

    private void OnDisable()
    {
        Speed = nomalSpeed;
    }

    private void Update()
    {
        rigid.linearVelocity = speed * transform.up;
    }
}
