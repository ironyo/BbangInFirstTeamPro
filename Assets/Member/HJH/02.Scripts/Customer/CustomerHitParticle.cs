using UnityEngine;

public class CustomerHitParticle : MonoBehaviour
{
    private ParticleSystem hitParticle;

    private void OnEnable()
    {
        hitParticle = GetComponentInChildren<ParticleSystem>();
    }
    public void HitParticle()
    {
        hitParticle.Play();
    }
}
