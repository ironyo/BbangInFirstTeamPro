using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretLabel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TurretNameTxt;
    [SerializeField] private TextMeshProUGUI CostTxt;
    [SerializeField] private Image TurretImage;

    [field: SerializeField] public DragButton PurchaseBtn;

    public void SetLabel(string _name, int _cost, Sprite _image)
    {
        TurretNameTxt.text = _name;
        CostTxt.text = _cost.ToString() + "¿ø";
        TurretImage.sprite = _image;
    }
}
