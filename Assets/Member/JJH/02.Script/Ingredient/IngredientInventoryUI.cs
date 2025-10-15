using UnityEngine;
using UnityEngine.InputSystem;

public class IngredientInventoryUI : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject InventoryImagePrefab;

    private ShowType showType = ShowType.Hide;

    [SerializeField] private IngredientSO potato;
    [SerializeField] private IngredientSO pepper;

    enum ShowType
    {
        Show,
        Hide
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            showType = showType == ShowType.Show ? ShowType.Hide : ShowType.Show;
        }

        InventoryType();
    }

    private void InventoryType()
    {
        if (showType == ShowType.Show)
        {
            InventoryShow();
        }
        else if (showType == ShowType.Hide)
        {
        }
    }

    private void InventoryShow()
    {
    }

    #region Button
    public void AddPotatoButton()
    {
        IngredientInventoryManager.Instance.AddInventoryIngredient(potato, 1);
    }

    public void AddPepperButton()
    {
        IngredientInventoryManager.Instance.AddInventoryIngredient(pepper, 1);
    }
    #endregion
}
