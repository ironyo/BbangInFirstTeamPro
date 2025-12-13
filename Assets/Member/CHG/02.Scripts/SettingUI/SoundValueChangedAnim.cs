using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundValueChangedAnim : MonoBehaviour
{
    private Image _img;
    private Animator _animator;
    [SerializeField] private Slider _slider;
    private float _lastValue;
    private bool _isPointing = false;
    private bool _isIdle = false;
    private float _stopTimer = 0f;
    [SerializeField] private float _stopDuraction = 0.2f;

    private readonly int ValueUpHash = Animator.StringToHash("ValueUp");
    private readonly int ValueDownHash = Animator.StringToHash("ValueDown");
    private void Start()
    {
        _img = GetComponent<Image>();
        _animator = GetComponent<Animator>();
        _lastValue = _slider.value;
        _img.DOFade(0, 0);
        OnPointerUp();
    }
    private void Update()
    {
        if (!_isPointing || _isIdle)
        {
            return;
        }

        if (Mathf.Abs(_slider.value - _lastValue) > 0.0001f)
        {
            _stopTimer = 0f;
            _isIdle = false; 
        }
        else 
        {
            _stopTimer += Time.deltaTime; 

            if (_stopTimer >= _stopDuraction)
            {
                if (!_isIdle)
                {
                    _animator.SetBool(ValueUpHash, false);
                    _animator.SetBool(ValueDownHash, false);

                    _isIdle = true;
                }
            }
        }

    }
    public void SetAnim()
    {
        if (_slider.value > _lastValue)
        {
            Debug.Log("Up");
            _animator.SetBool(ValueDownHash, false);
            _animator.SetBool(ValueUpHash, true);
        }
        else if (_slider.value < _lastValue)
        {
            Debug.Log("Down");
            _animator.SetBool(ValueUpHash, false);
            _animator.SetBool(ValueDownHash, true);
        }

        _isIdle = false;
        _stopTimer = 0f;

        if (_slider.value != _lastValue)
        {
            _lastValue = _slider.value;
        }
    }

    public void OnPointerUp()
    {
        _isPointing = false;
        _isIdle = true;
        _animator.SetBool(ValueUpHash, false);
        _animator.SetBool(ValueDownHash, false);
        _animator.enabled = false;
        _img.DOFade(0, 0.3f);
    }
    public void OnPointerDown()
    {
        _animator.enabled = true;
        _isPointing = true;
        _img.DOFade(1, 0.3f);
    }

}
