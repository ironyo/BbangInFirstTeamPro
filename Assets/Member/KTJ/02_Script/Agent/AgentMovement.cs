using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Rigidbody2D _rigid;

    private Vector2 _targetPos;
    private bool _isMovement;
    private float _speed;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isMovement)
        {
            if (Vector2.Distance(_rigid.position, _targetPos) > 0.01f)
            {
                Vector2 newPos = Vector2.MoveTowards(
                    _rigid.position,
                    _targetPos,
                    _speed * Time.fixedDeltaTime);

                _rigid.MovePosition(newPos);
            }
            else
            {
                _rigid.MovePosition(_targetPos);
                _isMovement = false;
                _animator.SetBool("OnWalk", false);
            }
        }
    }

    public void MoveTo(Vector2 _targetPos, float _speed)
    {
        _isMovement = true;
        this._targetPos = _targetPos;
        this._speed = _speed;
        _animator.SetBool("OnWalk", true);
    }
}
