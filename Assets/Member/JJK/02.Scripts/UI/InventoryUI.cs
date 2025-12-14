using System;
using UnityEngine;

public class InventoryUI : MonoSingleton<InventoryUI>
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    
    private InventorySlotUI[] _slots;

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += Refresh;
        CreateSlots();
        Refresh();
    }

    public RectTransform GetSlotRect(int index)
    {
        return _slots[index].GetComponent<RectTransform>();
    }
    
    private void CreateSlots()
    {
        int slotCount = InventoryManager.Instance.Items.Length;
        _slots = new InventorySlotUI[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            GameObject obj = Instantiate(slotPrefab, slotParent);
            _slots[i] = obj.GetComponent<InventorySlotUI>();
        }
    }
    
    public void Refresh()
    {
        var items = InventoryManager.Instance.Items;

        for (int i = 0; i < _slots.Length; i++)
        {
            if (items[i] != null)
                _slots[i].Setup(items[i]);
            else
                _slots[i].Clear(); 
        }
    }
}
