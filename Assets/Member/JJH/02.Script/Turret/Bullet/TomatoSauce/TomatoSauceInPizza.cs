using UnityEngine;

public class TomatoSauceInPizza : MonoBehaviour
{
    public int damage { get; set; }


    private void OnEnable()
    {
        ShotTomatoSauce();
    }

    private void ShotTomatoSauce()
    {
        LaserFade laserFade = GetComponent<LaserFade>();
        laserFade.enabled = true;
        laserFade.damage = damage;
    }
}
