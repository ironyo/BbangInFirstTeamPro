using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class RoadMoveManager : MonoSingleton<RoadMoveManager>
{
    [SerializeField] private Transform[] _roads;
    [SerializeField] private float _speed; // 자동차의 가속/감속으로 빼둬야함 <- 고민중

    [Header("UI Settings")]
    [SerializeField] private Slider _roadSlider;
    [SerializeField] private TextMeshProUGUI _leftRoadLength;

    private float _currentSpeed = 0;
    private float _roadObjectLength;
    private float _currentTime = 0;
    private float _targetedTime = 0;
    private bool _isMove = false;
    private bool _isDecreasing = false;

    private void OnEnable()
    {
        StageManager.Instance.OnStageRoadStart += StartMove;
       
    }

    private void OnDisable()
    {
        StageManager.Instance.OnStageRoadStart -= StartMove;
    }

    private void Start()
    {
        _roadObjectLength = _roads[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public void StartMove(int roadLength)
    {
        Debug.Log("도로 무빙 시작");
        _targetedTime = roadLength;
        _roadSlider.maxValue = roadLength;
        _roadSlider.value = 0;
        _isMove = true;

        StartCoroutine(SpeedUp(true, () =>
        {
            Debug.Log("속도 증가 완료");
        }));
    }


    public void StopMove()
    {
        Debug.Log("도로 무빙 끝남, 속도 감속중...");
        _isDecreasing = true;
        StartCoroutine(SpeedUp(false, () =>
        {
            _isDecreasing = false;
            _currentTime = 0;
            Debug.Log("속도 감소 완료");
            StageManager.Instance.OnStageRoadEnd?.Invoke();
            _isMove = false;
        }));
    }


    private void Update()
    {
        if (_isMove == true)
        {
            foreach (Transform road in _roads)
            {
                road.position += Vector3.left * _currentSpeed * Time.deltaTime;
                if (road.position.x <= -_roadObjectLength)
                {
                    road.position += Vector3.right * _roadObjectLength * _roads.Length;
                }
            }

            if (_isDecreasing == false)
            {
                _currentTime += Time.deltaTime;
                _roadSlider.value = _currentTime;
                float leftDistance = _targetedTime - _currentTime;

                _leftRoadLength.text = "남은거리: "+leftDistance.ToString("F1") + "m";
                if (_currentTime > _targetedTime)
                {
                    StopMove();
                }
            }
        }
    }

    private IEnumerator SpeedUp(bool isSpeedUp, Action callBack)
    {
        float duration = 2f;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            if (isSpeedUp)
            {
                _currentSpeed = Mathf.Lerp(0f, _speed, t);
            }
            else
            {
                _currentSpeed = Mathf.Lerp(_speed, 0f, t);
            }

            yield return null;
        }

        callBack?.Invoke();
    }
}
