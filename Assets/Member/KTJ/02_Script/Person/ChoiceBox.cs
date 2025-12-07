using UnityEngine;

public class ChoiceBox : MonoBehaviour
{
    public void RepairModeClick()
    {
        StoreManager_TJ.Instance.Enter(StoreEnum.Repair);
    }
    public void SellModeClick()
    {
        StoreManager_TJ.Instance.Enter(StoreEnum.Grocery);
    }
}