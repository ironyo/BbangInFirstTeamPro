using UnityEngine;

public class HitParticle : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
        }
    }
}
