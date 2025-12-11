using UnityEngine;

public class CustomerHitParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticle;

    public void PlayAt(Vector3 position)
    {
        hitParticle.transform.position = position;
        hitParticle.Play();
        Debug.Log(hitParticle);
    }
}
