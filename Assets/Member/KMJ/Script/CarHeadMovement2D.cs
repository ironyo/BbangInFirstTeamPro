using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarHeadMovement2D : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float moveSpeed = 5f;       // 최고 속도
    [SerializeField] private float rotationSpeed = 180f; // 도/초
    [SerializeField] private float acceleration = 6f;    // 가속 (m/s^2)
    [SerializeField] private float deceleration = 8f;    // 감속 (m/s^2)

    [Header("State")]
    public bool canMove = true;

    private Rigidbody2D _rb;
    private float _currentSpeed;
    private Vector2 _moveDir;
    private bool _isMoving;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.angularDamping = 0f;
        _rb.linearDamping = 0f;
    }

    private void Update()
    {
        if (!canMove)
        {
            _moveDir = Vector2.zero;
            _isMoving = false;
            return;
        }

        // W/S 전후, A/D 좌우 회전
        bool forward = Input.GetKey(KeyCode.W);
        bool backward = Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        _isMoving = false;
        _moveDir = Vector2.zero;

        if (forward)
        {
            _moveDir = transform.up;
            _isMoving = true;

            if (left) transform.rotation = Quaternion.Euler(0, 0,
                transform.eulerAngles.z + rotationSpeed * Time.deltaTime);
            if (right) transform.rotation = Quaternion.Euler(0, 0,
                transform.eulerAngles.z - rotationSpeed * Time.deltaTime);
        }
        else if (backward)
        {
            _moveDir = -transform.up;
            _isMoving = true;

            if (left) transform.rotation = Quaternion.Euler(0, 0,
                transform.eulerAngles.z - rotationSpeed * Time.deltaTime);
            if (right) transform.rotation = Quaternion.Euler(0, 0,
                transform.eulerAngles.z + rotationSpeed * Time.deltaTime);
        }

        // 속도 가감속
        float target = _isMoving ? moveSpeed : 0f;
        float rate = _isMoving ? acceleration : deceleration;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, target, rate * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        _rb.linearVelocity = _moveDir * _currentSpeed;
    }
}