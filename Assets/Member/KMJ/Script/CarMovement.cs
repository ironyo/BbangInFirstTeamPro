using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 moveDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveDir = transform.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDir = -transform.up;
        }
        else
        {
            moveDir = Vector2.zero;
        }

        
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = moveDir * moveSpeed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles += new Vector3(0, 0, rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles += new Vector3(0, 0, -rotationSpeed);
        }
    }
}
