using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruckSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _truckSlotNumTxt;
    [SerializeField] private Button _addBtn;

    public void SetSlotNum(int val)
    {
        _truckSlotNumTxt.text = val + "¹ø";
    }

    public Button GetAddBtn() => _addBtn;
}
