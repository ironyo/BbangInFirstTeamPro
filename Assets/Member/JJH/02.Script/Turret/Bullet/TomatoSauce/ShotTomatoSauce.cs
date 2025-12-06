using UnityEngine;

public class ShotTomatoSauce : MonoBehaviour, IShotBullet
{
    [SerializeField] private TomatoSauce tomatoSauce;


    public void ShotBullet()
    {
        tomatoSauce.ShotTomatoSauce();
    }
}
