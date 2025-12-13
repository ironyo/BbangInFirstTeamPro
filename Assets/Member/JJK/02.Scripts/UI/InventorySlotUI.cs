using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    private GameObject _skillInstance;

    public void Setup(ItemDataSO data)
    {
        Clear();
        icon.enabled = true;
        icon.sprite = data.Icon;
        
        _skillInstance = Instantiate(data.Prefab, transform);
        var skill = _skillInstance.GetComponent<SlotSkillBase>();
        skill.BindSlot(this);
    }

    public void Clear()
    {
        icon.enabled = false;
    }
}
