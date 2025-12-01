using System;
using UnityEngine;

public class ChangeForm : MonoBehaviour
{
    [SerializeField] private GameObject form;
    
    private SpriteRenderer spriter;
    private Sprite originSprite;
    
    private Drag drag;

    private void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        drag = GetComponent<Drag>();
    }

    private void Start()
    {
        originSprite = spriter.sprite;
    }

    private void Update()
    {
        if (drag.isDragging)
        {
            //spriter.sprite = form.GetComponent<SpriteRenderer>().sprite;
            form.SetActive(true);
        }
        else
        {
            //spriter.sprite = originSprite;
            form.SetActive(false);
        }
    }
}
