using UnityEngine;

public class RepairStore : Store
{
    public override StoreEnum StoreType => StoreEnum.Repair;
    private void Start()
    {
        StoreManager_TJ.Instance.Register(this);
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("정비소 입장");
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("정비소 퇴장");
    }
}
