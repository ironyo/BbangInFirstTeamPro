using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    [SerializeField] private int inventorySize = 20;
    public ItemDataSO[] Items { get; set; }

    public event Action OnInventoryChanged;

    protected override void Awake()
    {
        base.Awake();
        Items = new ItemDataSO[inventorySize];
    }

    public int GetFirstEmptySlot()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
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
        Items[slotIndex] = data;
        OnInventoryChanged?.Invoke();
    }
}
