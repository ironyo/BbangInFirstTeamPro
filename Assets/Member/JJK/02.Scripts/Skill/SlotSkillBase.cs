using System;
using UnityEngine;

public abstract class SlotSkillBase : MonoBehaviour
{
    protected InventorySlotUI _slot;

    public void BindSlot(InventorySlotUI slot)
    {
        _slot = slot;
    }

    private void OnDisable()
    {
        if (_slot != null)
        {
            _slot.Clear();
            Debug.Log("clear");
        }
    }
}
