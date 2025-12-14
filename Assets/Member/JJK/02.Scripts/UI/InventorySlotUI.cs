using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image backGroundIcon;
    private GameObject _skillInstance;
    private ItemDataSO _itemData;
    private float _timer;
    private MouseEnterExit _mouseEnterExit;

    private int _slotIndex;
    public void SetSlotIndex(int index) => _slotIndex = index;
    public ItemDataSO CurrentItem => _itemData;

    private void Awake()
    {
        _mouseEnterExit = GetComponent<MouseEnterExit>();
        _mouseEnterExit.OnMouseEnter += () => ToolTipManager.Instance.ShowToolTip(_itemData.Description);
    }

    public void Setup(ItemDataSO data)
    {
        if (_skillInstance != null)
            Destroy(_skillInstance);
        
        icon.sprite = data.Icon;
        icon.enabled = true;
        backGroundIcon.enabled = true;
        backGroundIcon.sprite = data.Icon;
        _itemData = data;
        
        _skillInstance = Instantiate(data.SkillPrefab, transform);
        var skill = _skillInstance.GetComponent<SlotSkillBase>();
        skill.BindSlot(this, _slotIndex);
    }

    private void Update()
    {
        if (icon.sprite != null)
        {
            _timer += Time.deltaTime;
            float fill = 1 - _timer / _itemData.Duration;
            icon.fillAmount = fill;
        }
    }

    public void ClearUIOnly()
    {
        icon.sprite = null;
        icon.enabled = false;

        backGroundIcon.enabled = false;

        _itemData = null;
        _timer = 0f;

        if (_skillInstance != null)
            Destroy(_skillInstance);

        _skillInstance = null;
    }
    
    public void ClearCompletely()
    {
        ClearUIOnly();
        InventoryManager.Instance.ClearSlot(_slotIndex);
    }
}
