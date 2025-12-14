using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image backGroundIcon;
    private GameObject _skillInstance;

    public void Setup(ItemDataSO data)
    {
        Clear();
        icon.enabled = true;
        icon.sprite = data.Icon;
        backGroundIcon.enabled = true;
        backGroundIcon.sprite = data.Icon;
        
        _skillInstance = Instantiate(data.Prefab, transform);
        var skill = _skillInstance.GetComponent<SlotSkillBase>();
        skill.BindSlot(this);
    }

    public void Clear()
    {
        icon.enabled = false;
        backGroundIcon.enabled = false;
        icon.sprite = null;

        if (_skillInstance != null)
            Destroy(_skillInstance);

        _skillInstance = null;
    }
}
