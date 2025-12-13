using DG.Tweening;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private ItemDataSO _data;
    private Transform _player;

    public void Init(ItemDataSO data)
    {
        _data = data;
    }

    public void PlayDropMotion(Vector3 start, Vector3 end, Vector3 control, float duration)
    {
        Vector3[] path = { start, control, end };

        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                MoveToInventorySlot();
            });
    }
    
    private void MoveToInventorySlot()
    {
        int emptySlot = InventoryManager.Instance.GetFirstEmptySlot();
    
        if (emptySlot < 0)
        {
            Destroy(gameObject);
            return;
        }
    
        RectTransform slotRect = InventoryUI.Instance.GetSlotRect(emptySlot);
        
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, slotRect.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;
    
        transform.DOMove(worldPos, 0.35f)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                InventoryManager.Instance.AddItemToSlot(_data, emptySlot);
                //Destroy(gameObject);
            });
    }
}
