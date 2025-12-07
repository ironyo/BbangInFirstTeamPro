using UnityEngine;

public class GroceryStore : Store
{
    public override StoreEnum StoreType => StoreEnum.Grocery;
    private void Start()
    {
        StoreManager_TJ.Instance.Register(this);
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("식료품점 입장");
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("식료품점 퇴장");
    }
}
