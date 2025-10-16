using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 moveDir;

    private bool canMove = false; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            canMove = true;
        }

        if (!canMove)
        {
            moveDir = Vector2.zero;
            return;
        }

        
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
        if (!canMove)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        _rb.linearVelocity = moveDir * moveSpeed;

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
