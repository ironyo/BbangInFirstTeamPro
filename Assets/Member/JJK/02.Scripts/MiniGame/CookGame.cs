using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CookGame : MonoBehaviour
{
    [SerializeField] private Slider cookSlider;
    [SerializeField] private GameObject handle;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float cookTime = 2f;
    [SerializeField] private RectTransform timingUI;
    [SerializeField] private float perfectRange = 0.05f;
    [SerializeField] private float goodRange = 0.1f;
    [SerializeField] private List<Color> colors;
    
    private bool isPlaying = false;
    private bool isFinished = false;
    
    private float value;
    private float targetValue;
    
    private GameObject cookingFood;
    private float cookingProgress = 0f;
    private Color _color;
    private CountDownUI _countDownUI;
    private bool isEntered = false;

    private void Awake()
    {
        _countDownUI = GetComponent<CountDownUI>();
        value = Mathf.Clamp(value, 0, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlaying = false;

            Judge();

            isFinished = true;
        }

        if (isPlaying)
        {
            value += Time.deltaTime * speed;
            cookSlider.value = value;
        }

        if (isFinished)
            SetColor();
    }

    private void Judge()
    {
        float diff = Mathf.Abs(targetValue - value);
            
        if (diff <= perfectRange)
        {
            StartCoroutine(_countDownUI.Result("Perfect"));
            _color = colors[2];
        }
        else if (diff <= goodRange)
        {
            StartCoroutine(_countDownUI.Result("Good"));
            
            if (value >= 0.5f)
                _color = colors[3];
            else
                _color = colors[1];
        }
        else
        {
            StartCoroutine(_countDownUI.Result("Bad"));
            
            if (value >= 0.5f)
                _color = colors[4];
            else
                _color = colors[0];
        }

        isEntered = false;
    }

    public IEnumerator GameStart()
    {
        SetRandomTimingZone();
        yield return StartCoroutine(_countDownUI.StartCountDown());
        
        value = 0;
        isPlaying = true;
        handle.SetActive(true);
    }

    private void SetRandomTimingZone()
    {
        targetValue = Random.Range(0f, 1f);

        float posX = Mathf.Lerp(-225, 225, targetValue);
        timingUI.anchoredPosition = new Vector2(posX, timingUI.anchoredPosition.y);

        targetValue = targetValue / 2 + 0.25f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (isEntered) return;
        isEntered = true;
        
        cookingFood = other.gameObject;
        StartCoroutine(GameStart());
    }

    private void SetColor()
    {
        MeshRenderer mr = cookingFood.GetComponent<MeshRenderer>();
        if (mr.material == null) return;
            
        if (mr.material.name.EndsWith("(Instance)") == false)
            mr.material = new Material(mr.material);
            
        cookingProgress += Time.deltaTime / cookTime; 
        cookingProgress = Mathf.Clamp01(cookingProgress);
        
        mr.material.color = Color.Lerp(Color.white, _color, cookingProgress);
    }
}
