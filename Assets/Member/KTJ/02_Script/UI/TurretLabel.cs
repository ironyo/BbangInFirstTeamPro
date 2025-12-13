using System;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretLabel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TurretNameTxt;
    [SerializeField] private TextMeshProUGUI CostTxt;
    [SerializeField] private Image TurretImage;

    [field: SerializeField] public DragButton PurchaseBtn;
    [SerializeField] private GameObject _lockGameobject;


    public void SetLabel(string _name, int _cost, Sprite _image, bool _isLock)
    {
        TurretNameTxt.text = _name;
        CostTxt.text = _cost.ToString() + "¿ø";
        TurretImage.sprite = _image;

        if (_isLock)
            LockLabel();
    }

    private void LockLabel()
    {
        _lockGameobject.SetActive(true);
    }
}
