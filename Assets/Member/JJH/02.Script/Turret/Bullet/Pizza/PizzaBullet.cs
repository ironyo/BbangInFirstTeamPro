using UnityEngine;

public class PizzaBullet : MonoBehaviour
{
    [SerializeField] private GameObject childrenBullet;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            for (int i = 0; i <= 360; i += 45)
            {
                Instantiate(childrenBullet, transform.position, Quaternion.Euler(0, 0, i));
            }
            Destroy(gameObject);
        }
    }
}
