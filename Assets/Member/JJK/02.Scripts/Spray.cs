using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spray : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float sens = 0.7f;
    [SerializeField] private float coollDown = 0.1f;
    [SerializeField] private float resetDelay = 1.5f;

    private Vector3 _velocity;
    private Vector3 _lastPos;
    private Vector3 _lastVelocity;
    private float _lastShakeTime;
    private int _sprayCount = 0;

    private void Update()
    {
        _velocity = (transform.position - _lastPos) /  Time.deltaTime;
        _lastPos = transform.position;
        
        if (_velocity.magnitude < 0.1f) return;
        
        Vector3 direction = _velocity.normalized;
        Vector3 lastDirection = _lastVelocity.normalized;
        
        float dirDot = Vector3.Dot(direction, lastDirection);

        if (dirDot < -sens && Time.time - _lastShakeTime > coollDown)
        {
            _sprayCount++;
            _lastShakeTime = Time.time;
        }
        
        if (Time.time - _lastShakeTime > resetDelay)
        {
            _sprayCount = 0;
        }

        if (_sprayCount > 2)
        {
            StartCoroutine(OnSpray());
        }
        
        _lastVelocity = _velocity;
    }

    private IEnumerator OnSpray()
    {
        var obj = Instantiate(particle, transform.position + Vector3.left, Quaternion.identity);
        obj.Emit(Random.Range(10, 15));
        _sprayCount = 0;

        yield return new WaitForSeconds(1f);
        
        Destroy(obj);
    }
}
