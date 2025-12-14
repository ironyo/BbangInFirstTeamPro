using System;
using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    [SerializeField] private int _money = 0;

    private float _moneyMultiplier = 1f;
    public void AddMultiplier(float value) => _moneyMultiplier += value;
    public void RemoveMultiplier(float value) => _moneyMultiplier -= value;

    public Action<int, int> OnMoneyChanged;

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            _money = Mathf.Clamp(_money, 0, 9999999);
        }

    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        AddMoney(60);
    }

    public void AddMoney(int amount)
    {
        int old = Money;

        float rate = 1f + (_moneyMultiplier * 0.01f);

        int finalAmount = Mathf.RoundToInt(amount * rate);
        _money += finalAmount;

        OnMoneyChanged?.Invoke(old, _money);
    }

    public bool SpendMoney(int amount)
    {
        if (amount <= 0 || _money < amount) return false;

        int old = Money;
        _money -= amount;
        OnMoneyChanged?.Invoke(old, _money);

        return true;
    }


    [ContextMenu("Money Plus")]
    private void MoneyPlus()
    {
        Money += 1000;
    }
}
