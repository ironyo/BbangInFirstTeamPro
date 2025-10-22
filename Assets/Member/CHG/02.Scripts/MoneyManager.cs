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
            Debug.Log(value);
            _money = value;
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
