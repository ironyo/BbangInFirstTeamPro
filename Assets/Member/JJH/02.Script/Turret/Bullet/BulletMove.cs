using UnityEngine;

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
        rigid.linearVelocity = speed * Vector3.up;
    }
}
