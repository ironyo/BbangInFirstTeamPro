using System;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    [SerializeField] private int inventorySize = 20;
    [NonSerialized] public ItemDataSO[] Items;

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
    
    public void ClearSlot(int index)
    {
        Items[index] = null;
        OnInventoryChanged?.Invoke();
    }
}
