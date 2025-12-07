using UnityEngine;

public class TomatoSauceInPizza : MonoBehaviour
{
    private void OnEnable()
    {
        ShotTomatoSauce();
    }

    private void ShotTomatoSauce()
    {
        GetComponent<LaserFade>().enabled = true;
    }
}
