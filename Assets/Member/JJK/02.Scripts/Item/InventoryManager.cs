using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    [SerializeField] private int inventorySize = 20;
    public ItemDataSO[] items;

    public event Action OnInventoryChanged;

    protected override void Awake()
    {
        base.Awake();
        items = new ItemDataSO[inventorySize];
    }

    public int GetFirstEmptySlot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                return i;
        }
        return -1;
    }
    
    public bool IsFull()
    {
        return GetFirstEmptySlot() == -1;
    }

    public void AddItemToSlot(ItemDataSO data, int slotIndex)
    {
        items[slotIndex] = data;
        OnInventoryChanged?.Invoke();
    }
    
    public void ClearSlot(int index)
    {
        items[index] = null;
        OnInventoryChanged?.Invoke();
    }
}
