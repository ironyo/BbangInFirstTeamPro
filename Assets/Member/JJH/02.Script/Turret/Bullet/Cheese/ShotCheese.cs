using UnityEngine;
using UnityEngine.InputSystem;

public class ShotCheese : MonoBehaviour
{
    [SerializeField] private GameObject cheese;
    private float spread = 15f;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnCheese(transform.rotation);
            SpawnCheese(transform.rotation * Quaternion.Euler(0, 0, -spread));
            SpawnCheese(transform.rotation * Quaternion.Euler(0, 0, spread));
        }
    }

    private void SpawnCheese(Quaternion quaternion)
    {
        Instantiate(cheese, transform.position, quaternion);
    }
}
