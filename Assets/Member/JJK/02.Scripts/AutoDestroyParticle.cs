using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    private ParticleSystem[] particles;

    private void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    private void Update()
    {
        foreach (var ps in particles)
        {
            if (ps.IsAlive(true))
            {
                return;
            }
        }

        Destroy(gameObject);
    }
}
