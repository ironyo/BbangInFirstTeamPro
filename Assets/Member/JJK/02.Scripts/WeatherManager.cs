using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private float minCoolTime;
    [SerializeField] private float maxCoolTime;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    [SerializeField] private ParticleSystem rainParticle;
    [SerializeField] private ParticleSystem snowParticle;
    
    private ParticleSystem _currentParticle;
    private float _coolTime;
    private float _duration;

    private int _lastWeather = -1;
    private int _count = 0;

    private void Start()
    {
        StartCoroutine(WeatherCoroutine());
    }

    private void SetWeather()
    {
        int newWeather =  Random.Range(0, 2); //0: rain, 1: Snow

        if (newWeather == _lastWeather)
        {
            _count++;

            if (_count == 2)
            {
                newWeather = newWeather == 0 ? 1 : 0;
                _count = 0;
            }
        }
        else
            _count = 0;
        
        _lastWeather = newWeather;

        if (newWeather == 0)
            _currentParticle = rainParticle;
        else if (newWeather == 1)
            _currentParticle = snowParticle;
        
        _coolTime = Random.Range(minCoolTime, maxCoolTime);
        _duration = Random.Range(minDuration, maxDuration);
    }

    private IEnumerator WeatherCoroutine()
    {
        while (true)
        {
            SetWeather();
        
            yield return new WaitForSeconds(_coolTime);
        
            Debug.Log("Play");
            _currentParticle.Play();
        
            yield return new WaitForSeconds(_duration);
        
            _currentParticle.Stop();
        }
    }
}
