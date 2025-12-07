using UnityEngine;

public class PizzaPieceBullet : MonoBehaviour
{
    [SerializeField] private GameObject cheeseExplosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(cheeseExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
