using UnityEngine;

public class CheeseExplosion : MonoBehaviour
{
    [SerializeField] private GameObject cheesePuddle;

    public void ExplosionDestroy()
    {
        Instantiate(cheesePuddle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
