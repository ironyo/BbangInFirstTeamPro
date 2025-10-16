using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookGame : MonoBehaviour
{
    [SerializeField] private Slider cookSlider;
    [SerializeField] private GameObject handle;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float cookTime = 2f;
    [SerializeField] private List<Color> colors;
    
    private bool isPlaying = false;
    private bool isFinished = false;
    
    private float value;
    private GameObject cookingFood;
    private float cookingProgress = 0f;
    private Color _color;
    
    float[] sections = { 0.055f, 0.277f, 0.333f, 0.555f };
    string[] results = { "Bad", "Good", "Perfect", "Good", "Bad" };
    int index = 0;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlaying = false;

            if (value < sections[0]) index = 0;
            else if (value < sections[1]) index = 1;
            else if (value < sections[2]) index = 2;
            else if (value < sections[3]) index = 3;
            else index = 4;
            
            Debug.Log(results[index]);
            _color = colors[index];

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

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        value = 0;
        isPlaying = true;
        handle.SetActive(true);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        cookingFood = collision.gameObject;
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
