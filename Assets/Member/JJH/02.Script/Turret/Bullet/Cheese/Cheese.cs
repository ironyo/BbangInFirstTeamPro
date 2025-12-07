using UnityEngine;

public class Cheese : MonoBehaviour
{
    [SerializeField] private GameObject cheeseExplosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CameraShake.Instance.ImpulseForce(0.1f);

            Instantiate(cheeseExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
