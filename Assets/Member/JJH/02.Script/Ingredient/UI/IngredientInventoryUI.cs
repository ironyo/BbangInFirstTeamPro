using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IngredientInventoryUI : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject inventoryImagePrefab;
    [SerializeField] private GameObject inventoryUIParent;
    [SerializeField] private RectTransform leftWindowRect;
    [SerializeField] private RectTransform rightWindowRect;

    private IngredientInformationUI ingredientInformationUI;

    private List<GameObject> viewGameObjectList = new List<GameObject>();
    private List<IngredientSO> viewList = new List<IngredientSO>();

    private ShowType showType = ShowType.Hide;

    [SerializeField] private IngredientSO potato;
    [SerializeField] private IngredientSO pepper;
    [SerializeField] private IngredientSO carrot;

    enum ShowType
    {
        Show,
        Hide
    }

    private void Awake()
    {
        ingredientInformationUI = rightWindowRect.gameObject.GetComponent<IngredientInformationUI>();
    }

    private void OnEnable()
    {
        IngredientInventoryManager.Instance.OnInventoryChanged += InventoryShow;
    }

    private void OnDisable()
    {
        if (IngredientInventoryManager.Instance != null)
            IngredientInventoryManager.Instance.OnInventoryChanged -= InventoryShow;
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
            leftWindowRect.DOAnchorPos(new Vector2(-600, -30), 0.7f);
            rightWindowRect.DOAnchorPos(new Vector2(600, -30), 0.7f);
        }
        else if (showType == ShowType.Hide)
        {
            leftWindowRect.DOAnchorPos(new Vector2(-1400, -30), 0.7f);
            rightWindowRect.DOAnchorPos(new Vector2(1400, -30), 0.7f);
        }
    }

    private void InventoryShow()
    {
        var infoList = GetComponentsInChildren<IngredientImageUI>(true);

        foreach (KeyValuePair<IngredientSO, int> ingredient in IngredientInventoryManager.Instance.ingredientDictionary)
        {
            if (viewList.Contains(ingredient.Key))
            {
                int idx = viewList.IndexOf(ingredient.Key);
                IngredientImageUI information = viewGameObjectList[idx].GetComponent<IngredientImageUI>();
                information.Create(ingredient.Key, ingredient.Value);
            }
            else
            {
                GameObject prefab = Instantiate(inventoryImagePrefab, inventoryUIParent.transform);
                IngredientImageUI information = prefab.GetComponent<IngredientImageUI>();
                information.Create(ingredient.Key, ingredient.Value);

                viewGameObjectList.Add(prefab);
                viewList.Add(ingredient.Key);
            }
        }
    }

    public void ShowIngredientInformation(IngredientSO ingredient)
    {
        ingredientInformationUI.ShowIngredientInformation(ingredient);
    }

    public void HideIngredientInformation()
    {
        ingredientInformationUI.HideIngredientInformation();
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
    public void AddCarrotButton()
    {
        IngredientInventoryManager.Instance.AddInventoryIngredient(carrot, 1);
    }
    #endregion
}
