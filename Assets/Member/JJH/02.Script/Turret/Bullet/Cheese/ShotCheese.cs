using UnityEngine;

public class ShotCheese : MonoBehaviour, IShotBullet
{
    [SerializeField] private GameObject cheese;
    private float spread = 15f;

    public void ShotBullet()
    {
        SpawnCheese(transform.rotation);
        SpawnCheese(transform.rotation * Quaternion.Euler(0, 0, -spread));
        SpawnCheese(transform.rotation * Quaternion.Euler(0, 0, spread));
    }

    private void SpawnCheese(Quaternion quaternion)
    {
        Instantiate(cheese, transform.position, quaternion);
    }
}
