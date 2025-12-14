using System;
using UnityEngine;

public abstract class SlotSkillBase : MonoBehaviour
{
    protected InventorySlotUI slot;

    public void BindSlot(InventorySlotUI slot)
    {
        this.slot = slot;
    }

    private void OnDisable()
    {
        slot.Clear();
    }
}
