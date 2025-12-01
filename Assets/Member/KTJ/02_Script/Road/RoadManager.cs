using System;
using System.Collections;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public event Action<float> OnLeftDistanceChanged;
    public event Action OnRoadFinished;
    public event Action<float> OnSpeedChanged;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    private float _currentSpeed = 0f;
    public float CurrentSpeed => _currentSpeed;

    private float _currentTime = 0;
    private float _targetedTime = 0;
    private bool _isMoving = false;
    private bool _isDecreasing = false;

    private void OnEnable()
    {
        StageManager.Instance.OnStageRoadStart += StartMove;
    }

    private void OnDisable()
    {
        StageManager.Instance.OnStageRoadStart -= StartMove;
    }

    public void StartMove(int roadLength)
    {
        _targetedTime = roadLength;
        _currentTime = 0;
        _isMoving = true;

        StartCoroutine(SpeedUp(true, () =>
        {
            Debug.Log("속도 증가 완료");
        }));
    }

    public void StopMove()
    {
        _isDecreasing = true;

        StartCoroutine(SpeedUp(false, () =>
        {
            _isDecreasing = false;
            _isMoving = false;
            OnRoadFinished?.Invoke();
            StageManager.Instance.OnStageRoadEnd.Invoke();
        }));
    }

    private void Update()
    {
        if (_isMoving == false) return;

        if (!_isDecreasing)
        {
            _currentTime += Time.deltaTime;
            float leftDistance = _targetedTime - _currentTime;

            OnLeftDistanceChanged?.Invoke(leftDistance);

            if (_currentTime > _targetedTime)
                StopMove();
        }
    }

    private IEnumerator SpeedUp(bool isUp, Action callback)
    {
        float duration = 2f;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            _currentSpeed = isUp
                ? Mathf.Lerp(0f, _speed, t)
                : Mathf.Lerp(_speed, 0f, t);

            OnSpeedChanged?.Invoke(_currentSpeed);

            yield return null;
        }

        callback?.Invoke();
    }
}
