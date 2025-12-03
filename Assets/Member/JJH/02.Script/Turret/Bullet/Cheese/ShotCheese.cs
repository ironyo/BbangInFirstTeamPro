using UnityEngine;
using UnityEngine.InputSystem;

public class ShotCheese : MonoBehaviour
{
    [SerializeField] private GameObject cheese;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShotCheeses();
        }
    }

    private void ShotCheeses()
    {

        Instantiate(cheese);
    }
}
