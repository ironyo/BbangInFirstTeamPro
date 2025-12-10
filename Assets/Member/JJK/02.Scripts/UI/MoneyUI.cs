using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private DigitSlot[] slots;
    [SerializeField] private int testMoney = 1;
    
    private void Start()
    {
        MoneyManager.Instance.OnMoneyChanged += UpdateMoney;
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            MoneyManager.Instance.AddMoney(testMoney);
        }
    }

    private void UpdateMoney(int oldMoney, int newMoney)
    {
        string oldStr = oldMoney.ToString().PadLeft(slots.Length, '0');
        string newStr = newMoney.ToString().PadLeft(slots.Length, '0');
        
        int oldLen = oldMoney.ToString().Length;

        for (int i = 0; i < slots.Length ; i++)
        {
            int oldDigit = oldStr[i] - '0';
            int newDigit = newStr[i] - '0';
            
            if (oldDigit != newDigit)
            {
                if (i >= oldLen)
                {
                    // 그냥 자리 값만 세팅
                    slots[i].SetInstant(newDigit);
                    continue;
                }
                
                slots[i].PlayChange(oldDigit, newDigit);
            }
        }
    }
}
