using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private DigitSlot[] slots;
    [SerializeField] private int testMoney = 1;
    
    private void Awake()
    {
        MoneyManager.Instance.OnMoneyChanged += UpdateMoney;
    }

    private void UpdateMoney(int oldMoney, int newMoney)
    {
        string oldStr = oldMoney.ToString().PadLeft(slots.Length, '0');
        string newStr = newMoney.ToString().PadLeft(slots.Length, '0');
        
        int oldLen = oldMoney.ToString().Length;
        int newLen = newMoney.ToString().Length;
    
        for (int i = 0; i < slots.Length ; i++)
        {
            int oldDigit = oldStr[i] - '0';
            int newDigit = newStr[i] - '0';
            
            int oldIndex = slots.Length - oldLen - 1;
            int newIndex = slots.Length - newLen - 1;
            
            if (i <= newIndex && newDigit == 0)
            {
                slots[i].SetEmpty();
            }
            else if (oldDigit != newDigit)
            {
                if (i <= oldIndex)
                    slots[i].PlayChange(oldDigit, newDigit, false);
                else
                    slots[i].PlayChange(oldDigit, newDigit, true);
            }
            else if (i > newIndex && !slots[i].GetComponent<DigitSlot>().isDigit)
            {
                slots[i].PlayChange(oldDigit, newDigit, false);
            }
        }
    }
}
