using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 5f;


    

    public bool canMove = true; 

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (canMove)
        //        canMove = false;
        //    else
        //        canMove = true;
        //}
        if (!canMove) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }
}
