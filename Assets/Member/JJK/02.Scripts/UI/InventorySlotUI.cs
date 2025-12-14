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

    public void Setup(ItemDataSO data)
    {
        Clear();
        icon.enabled = true;
        icon.sprite = data.Icon;
        backGroundIcon.enabled = true;
        backGroundIcon.sprite = data.Icon;
        _itemData = data;
        
        _skillInstance = Instantiate(data.Prefab, transform);
        var skill = _skillInstance.GetComponent<SlotSkillBase>();
        skill.BindSlot(this);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        float fill = 1 - _timer / _itemData.Duration;
        icon.fillAmount = fill;
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
