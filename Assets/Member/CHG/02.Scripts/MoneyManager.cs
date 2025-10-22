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
            _money += value;
        }

    }

    protected override void Awake()
    {
        base.Awake();
    }

    [ContextMenu("Money Plus")]
    private void MoneyPlus()
    {
        Money += 1000;
    }
}
