using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), (typeof(BoxCollider2D)))]
public class BulletMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private int speed = 5;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigid.linearVelocity = speed * transform.up;
    }
}
