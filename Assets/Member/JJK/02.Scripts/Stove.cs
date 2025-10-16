using System;
using UnityEngine;

public class Stove : MonoBehaviour
{
    [SerializeField] private Color rawColor = Color.white;
    [SerializeField] private Color cookedColor = new Color(0.6f, 0.3f, 0.1f);
    [SerializeField] private float cookTime = 5f;
    
    private float cookingProgress = 0f;
    private bool onStove = false;
    private GameObject cookingFood;

    private void Update()
    {
        if (onStove && cookingFood != null)
        {
            MeshRenderer mr = cookingFood.GetComponent<MeshRenderer>();
            if (mr.material == null) return;
        
            // Material 인스턴스 생성 (한 번만)
            if (mr.material.name.EndsWith("(Instance)") == false)
                mr.material = new Material(mr.material);
        
            cookingProgress += Time.deltaTime / cookTime;
            cookingProgress = Mathf.Clamp01(cookingProgress);
        
            mr.material.color = Color.Lerp(rawColor, cookedColor, cookingProgress);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onStove = true;
        cookingFood = collision.gameObject;
    }

    private void OnCollisionExit(Collision collision)
    {
        onStove = false;
        cookingFood = null;
    }
}
