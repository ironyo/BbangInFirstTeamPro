using UnityEngine;

public class ClearState : IEnemyState
{
    private Customer enemy;

    public ClearState(Customer enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {

    }

    public void Update() { }
    public void Exit() { }
}
