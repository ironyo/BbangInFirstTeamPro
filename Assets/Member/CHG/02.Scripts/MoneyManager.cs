using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{

    [SerializeField] private int _money = 0;

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            _money = Mathf.Clamp(_money, 0, 9999);
        }

    }

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddMoney(int index)
    {
        _money += index;
    }

    public bool SpendMoney(int index)
    {
        if (index <= 0 ||_money < index) return false;
       
        _money -= index;

        return true;
    }


    [ContextMenu("Money Plus")]
    private void MoneyPlus()
    {
        Money += 1000;
    }
}
