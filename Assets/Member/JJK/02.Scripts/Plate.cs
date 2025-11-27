using System;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.renderQueue = 1500;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("CuttingFood") && collision.transform.parent != transform)
        {
            collision.transform.parent = transform;
            Debug.Log("collison");
        }
    }
}
