using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spray : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float sens = 0.7f;
    [SerializeField] private float coollDown = 0.1f; //너무 자주 발사 방지
    [SerializeField] private float resetDelay = 1.5f;

    private Vector3 velocity;
    private Vector3 lastPos;
    private Vector3 lastVelocity;
    private float lastShakeTime;
    private int sprayCount = 0;

    private void Start()
    {
        //lastVelocity = velocity;
        //lastVelocity = Vector3.zero;
    }

    private void Update()
    {
        velocity = (transform.position - lastPos) /  Time.deltaTime;
        lastPos = transform.position;
        
        if (velocity.magnitude < 0.1f) return;
        
        Vector3 direction = velocity.normalized;
        Vector3 lastDirection = lastVelocity.normalized;
        
        float dirDot = Vector3.Dot(direction, lastDirection);

        if (dirDot < -sens && Time.time - lastShakeTime > coollDown)
        {
            sprayCount++;
            lastShakeTime = Time.time;
        }
        
        if (Time.time - lastShakeTime > resetDelay)
        {
            sprayCount = 0;
        }

        if (sprayCount > 2)
        {
            StartCoroutine(OnSpray());
        }
        
        lastVelocity = velocity;
    }

    private IEnumerator OnSpray()
    {
        var obj = Instantiate(particle, transform.position + Vector3.left, Quaternion.identity);
        obj.Emit(Random.Range(10, 15));
        sprayCount = 0;

        yield return new WaitForSeconds(1f);
        
        Destroy(obj);
    }
}
