using UnityEngine;

public class CustomerHitParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticle;
    float randomP;
    public void PlayAt(Vector3 position)
    {
        randomP = Random.Range(0f, 1.5f);
        hitParticle.transform.position = new Vector2(position.x + randomP,position.y);
        hitParticle.Play();
        Debug.Log(hitParticle);
    }
}
