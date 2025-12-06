using UnityEngine;

public class ShotPizza : MonoBehaviour, IShotBullet
{
    [SerializeField] private GameObject pizza;

    public void ShotBullet()
    {
        Instantiate(pizza, transform.position, transform.rotation);
    }
}
