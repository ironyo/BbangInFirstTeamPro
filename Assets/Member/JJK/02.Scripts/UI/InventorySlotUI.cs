using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void Setup(ItemDataSO data)
    {
        icon.enabled = true;
        icon.sprite = data.Icon;
    }

    public void Clear()
    {
        icon.enabled = false;
    }
}
