using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 moveDir;
    public CarMovement car;
    public bool canMove = false; 
    private bool isMoving = false;

    public PlayerMovement Player;
    private InteractNearObject2D InteractNearObject;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        InteractNearObject = GetComponentInChildren<InteractNearObject2D>();
    }

    private void Update()
    {
        if (!canMove)
        {
            moveDir = Vector2.zero;
            isMoving = false;
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveDir = transform.up;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDir = -transform.up;
            isMoving = true;
        }
        else
        {
            moveDir = Vector2.zero;
            isMoving = false;
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

        if (isMoving)
        {
            if (Input.GetKey(KeyCode.A))
                transform.eulerAngles += new Vector3(0, 0, rotationSpeed);

            if (Input.GetKey(KeyCode.D))
                transform.eulerAngles += new Vector3(0, 0, -rotationSpeed);
        }
    }
}
