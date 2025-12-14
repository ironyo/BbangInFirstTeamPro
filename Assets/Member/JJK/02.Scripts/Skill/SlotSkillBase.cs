using System;
using UnityEngine;

public abstract class SlotSkillBase : MonoBehaviour
{
    protected InventorySlotUI _slot;
    protected int SlotIndex;
    private bool _ended = false;

    public void BindSlot(InventorySlotUI slot, int index)
    {
        _slot = slot;
        SlotIndex = index;
    }

    private void OnDisable()
    {
        if (_ended) return;
        _ended = true;

        if (_slot != null)
            _slot.ClearCompletely();
    }
}
