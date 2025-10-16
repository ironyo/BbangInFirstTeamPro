using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    public CarMovement car; 

    private bool canMove = true; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            canMove = false;
            car.canMove = true;
            Debug.Log("차량에 탑승");
            return;
        }

        
        if (!canMove) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }
}
