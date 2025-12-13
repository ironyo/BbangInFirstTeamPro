using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private float _xMaxPos; // 6
    [SerializeField] private float _xMinPos; // -16

    // 합=22, 22 * 

    [Header("Ui Setting")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Slider _posSlider;

    [Header("Event")]
    [SerializeField] private EventChannelSO _onGameOver;

    private float _time = 0.1f;
    private float _currentTime;

    private float _camXPos;

    private Coroutine _coroutine;

    private bool _canMove = true;

    private void OnEnable()
    {
        _onGameOver.OnEventRaised += () => _canMove = false;
    }
    public float CamXPos
    {
        get
        {
            return _camXPos;
        }
        private set
        {
            _camXPos = Mathf.Clamp(value, _xMinPos, _xMaxPos);
        }
    }
    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); // -1, 0, 1

        if (Mathf.Abs(input) > 0)
        {
            if (_canMove == false) return;

            _currentTime += Time.deltaTime;
            if (_currentTime >= _time)
            {
                _currentTime = 0f;
                CamMove(input);
            }
        }
        else
        {
            _currentTime = 0f;
        }
    }


    IEnumerator CanvasGroupFade()
    {
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 0.5f);
        yield return new WaitForSeconds(3f);
        _canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            _canvasGroup.gameObject.SetActive(false);
        });
    }

    private void CamMove(float amount)
    {
        CamXPos += amount;
        _cam.transform.position = new Vector3(CamXPos, 0, 0);
        _posSlider.value = CalculatePrimeSliderValue(_xMinPos, CamXPos, _xMaxPos);

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(CanvasGroupFade());
    }

    float CalculatePrimeSliderValue(float minPrime, float currentPrime, float maxPrime)
    {
        // 방어 코드 (Invalid Argument 방지)
        if (maxPrime <= minPrime)
            throw new ArgumentException("maxPrime must be greater than minPrime");

        float normalized =
            (float)(currentPrime - minPrime) /
            (maxPrime - minPrime);

        return Mathf.Clamp01(normalized) * 100f;
    }

}
