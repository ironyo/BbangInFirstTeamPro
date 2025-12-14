using System;
using System.Collections;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] EventChannelSO _onRoadFinished;
    [SerializeField] EventChannelSO_T<float> OnLeftDistanceChanged;
    [SerializeField] EventChannelSO_T<float> OnSpeedChanged;
    [SerializeField] private EventChannelSO_T<int> _onStageRoadStart;
    [SerializeField] private EventChannelSO _onStageRoadEnd;

    public float Speed => _speed;

    private float _currentSpeed = 0f;
    public float CurrentSpeed => _currentSpeed;

    private float _currentTime = 0;
    private float _targetedTime = 0;
    private bool _isMoving = false;
    private bool _isDecreasing = false;

    private void OnEnable()
    {
        _onStageRoadStart.OnEventRaised += StartMove;
    }

    private void OnDisable()
    {
        _onStageRoadStart.OnEventRaised -= StartMove;
    }

    public void StartMove(int roadLength)
    {
        _targetedTime = roadLength;
        _currentTime = 0;
        _isMoving = true;

        StartCoroutine(SpeedUp(true, () =>
        {
            Debug.Log("속도 증가 완료");
            CameraMoverManager.Instance.UnlockCamMove();
        }));
    }

    public void StopMove()
    {
        _isDecreasing = true;

        StartCoroutine(SpeedUp(false, () =>
        {
            _isDecreasing = false;
            _isMoving = false;
            _onRoadFinished.RaiseEvent();
            _onStageRoadEnd.RaiseEvent();
        }));
    }

    private void Update()
    {
        if (_isMoving == false) return;

        if (!_isDecreasing)
        {
            _currentTime += Time.deltaTime;
            float leftDistance = _targetedTime - _currentTime;

            OnLeftDistanceChanged.RaiseEvent(leftDistance);

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

            OnSpeedChanged.RaiseEvent(_currentSpeed);

            yield return null;
        }

        callback?.Invoke();
    }
}
