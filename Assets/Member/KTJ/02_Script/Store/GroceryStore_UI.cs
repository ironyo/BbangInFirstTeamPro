using UnityEngine;

public class GroceryStore_UI : Store_UI
{
    [SerializeField] private GameObject UI;
    public override void CloseUI()
    {
        UI.SetActive(false);
    }

    public override void OpenUI()
    {
        UI.SetActive(true);
    }
}
