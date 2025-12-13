using System;
using UnityEngine;

public class InventoryUI : MonoSingleton<InventoryUI>
{
    [SerializeField] private RectTransform[] slotRects;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;

    private void Start()
    {
        Refresh();
    }

    private void OnEnable()
    {
        InventoryManager.Instance.OnInventoryChanged += Refresh;
    }

    public RectTransform GetSlotRect(int index)
    {
        return slotRects[index];
    }
    
    public void Refresh()
    {
        foreach (Transform pos in slotRects)
        {
            for (int i = pos.childCount - 1; i >= 0; i--)
                Destroy(pos.GetChild(i).gameObject);
        }

        ItemDataSO[] items = InventoryManager.Instance.Items;

        for (int i = 0; i < items.Length && i < slotRects.Length; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotRects[i]);
            InventorySlotUI ui = slot.GetComponent<InventorySlotUI>();

            if (items[i] != null)
                ui.Setup(items[i]);
            else
                ui.Clear();
        }
    }
}
