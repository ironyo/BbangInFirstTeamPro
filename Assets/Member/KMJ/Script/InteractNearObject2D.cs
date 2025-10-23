using UnityEngine;

public class InteractNearObject2D : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;               
    public float interactRange = 2f;       
    public KeyCode interactKey = KeyCode.F;

    public bool isNear = false;

    private CarMovement carMovement;

    private PlayerMovement playerMovement;

    [SerializeField] private GameObject player;

    private void Awake()
    {
        carMovement = GetComponentInParent<CarMovement>();
        playerMovement = player.GetComponent<PlayerMovement>();
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
        Debug.Log("a");
        carMovement.canMove = !carMovement.canMove;
        playerMovement.canMove = !playerMovement.canMove;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
