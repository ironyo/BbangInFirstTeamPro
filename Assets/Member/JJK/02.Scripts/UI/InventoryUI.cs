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
        int slotCount = InventoryManager.Instance.items.Length;
        _slots = new InventorySlotUI[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            GameObject obj = Instantiate(slotPrefab, slotParent);
            _slots[i] = obj.GetComponent<InventorySlotUI>();
            _slots[i].SetSlotIndex(i);
        }
    }
    
    public void Refresh()
    {
        var items = InventoryManager.Instance.items;

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].CurrentItem == items[i])
                continue;
            
            if (items[i] != null)
            {
                _slots[i].Setup(items[i]);
                Debug.Log("SetUp");
            }
            else
            {
                _slots[i].ClearUIOnly();
                Debug.Log("Clear");
            }
        }
    }
}
