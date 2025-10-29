using UnityEngine;

public class InteractNearObject2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float deceleration = 3f;

    private Rigidbody2D _rb;
    private Vector2 moveDir;
    private float currentSpeed = 0f;
    public bool canMove = false;
    private bool isMoving = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canMove)
        {
            moveDir = Vector2.zero;
            isMoving = false;
            return;
        }

        moveDir = Vector2.zero;
        isMoving = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveDir = transform.up;
            isMoving = true;

            if (Input.GetKey(KeyCode.A))
                transform.eulerAngles += new Vector3(0, 0, rotationSpeed);
            if (Input.GetKey(KeyCode.D))
                transform.eulerAngles += new Vector3(0, 0, -rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDir = -transform.up;
            isMoving = true;

            if (Input.GetKey(KeyCode.A))
                transform.eulerAngles += new Vector3(0, 0, -rotationSpeed);
            if (Input.GetKey(KeyCode.D))
                transform.eulerAngles += new Vector3(0, 0, rotationSpeed);
        }

        if (isMoving)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }


        if (!canMove)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        _rb.linearVelocity = moveDir * currentSpeed;
    }
}

