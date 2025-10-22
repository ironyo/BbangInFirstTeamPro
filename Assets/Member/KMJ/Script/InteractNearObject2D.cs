using UnityEngine;

public class InteractNearObject2D : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;              
    public float interactRange = 2f;      
    public KeyCode interactKey = KeyCode.F; 

    private bool isNear = false;

    void Update()
    {
        if (target == null) return;

        
        float distance = Vector2.Distance(transform.position, target.position);

        isNear = distance <= interactRange;

        if (isNear && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log($"{target.name}과(와) 상호작용했습니다!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
