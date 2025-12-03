using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), (typeof(BoxCollider2D)))]
public class BulletMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float speed = 5;
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
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigid.linearVelocity = speed * transform.up;
    }
}
