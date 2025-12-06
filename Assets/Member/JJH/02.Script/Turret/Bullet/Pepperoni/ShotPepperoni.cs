using UnityEngine;

public class ShotPepperoni : MonoBehaviour, IShotBullet
{
    [SerializeField] private GameObject pepperoni;

    public void ShotBullet()
    {
        Instantiate(pepperoni, transform.position, transform.rotation);
    }
}
