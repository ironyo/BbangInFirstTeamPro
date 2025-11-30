using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarHeadMovement2D : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float deceleration = 8f;

    [Header("Steering")]
    [SerializeField] private float steerAngle = 30f;
    [SerializeField] private float steerSpeed = 120f;        // 좌우 회전 속도
    [SerializeField] private float steerReturnSpeed = 60f;   // 복귀 속도 (느리게!)

    public bool canMove = true;

    private Rigidbody2D _rb;
    private float _currentSpeed;
    private Vector2 _moveDir;

    private float _currentSteer = 0f;

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
            _currentSpeed = 0f;
            return;
        }

        bool forward = Input.GetKey(KeyCode.W);
        bool backward = Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        // -----------------------
        // 좌/우 조향 + 자동 복귀
        // -----------------------
        if (left)
        {
            _currentSteer = Mathf.MoveTowards(
                _currentSteer,
                -steerAngle,
                steerSpeed * Time.deltaTime
            );  
        }
        else if (right)
        {
            _currentSteer = Mathf.MoveTowards(
                _currentSteer,
                steerAngle,
                steerSpeed * Time.deltaTime
            );
        }
        else
        {
            // 입력 없을 때 더 느린 속도로 복귀
            _currentSteer = Mathf.MoveTowards(
                _currentSteer,
                0f,
                steerReturnSpeed * Time.deltaTime
            );
        }

        // 회전 적용
        transform.rotation = Quaternion.Euler(0, 0, _currentSteer);

        Vector2 dir = Vector2.zero;
        bool isMoving = false;

        if (forward)
        {
            dir = transform.up;
            isMoving = true;
        }
        else if (backward)
        {
            dir = -transform.up;
            isMoving = true;
        }

        _moveDir = dir;

        float target = isMoving ? moveSpeed : 0f;
        float rate = isMoving ? acceleration : deceleration;

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
