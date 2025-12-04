using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TrailEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem trailEffect;
    [SerializeField] private float delay;

    private void OnEnable()
    {
        StartCoroutine(SpawnTrail());
    }

    private IEnumerator SpawnTrail()
    {
        var ptc =  Instantiate(trailEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnTrail());
    }
}
