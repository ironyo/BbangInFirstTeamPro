using UnityEngine;

public class InteractNearObject2D : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;               
    public float interactRange = 2f;       
    public KeyCode interactKey = KeyCode.F;

    private bool isNear = false;

    private CarMovement carMovement;

    private void Awake()
    {
        carMovement = GetComponentInParent<CarMovement>();
    }

    void Update()
    {
        if (target == null) return;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, LayerMask.GetMask("Player"));

        
        if (hit != null && hit)
        {
            isNear = true;
        }
        else
        {
            isNear = false;
        }

        if (isNear && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log($"{target.name}과(와) 상호작용했습니다!");
        carMovement.canMove = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
