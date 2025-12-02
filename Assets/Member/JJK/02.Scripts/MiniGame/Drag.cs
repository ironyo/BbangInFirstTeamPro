using UnityEngine;

public class Drag : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 offset;
    
    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, 0);
        isDragging = true;
    }
    
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
        }
    }
    
    private void OnMouseUp()
    {
        isDragging = false;
    }
}
