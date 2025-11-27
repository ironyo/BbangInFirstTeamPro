using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecipeInventoryUI : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject inventoryImagePrefab;
    [SerializeField] private RectTransform leftWindowRect;
    [SerializeField] private RectTransform rightWindowRect;

    [Header("Data")]
    [SerializeField] private RecipeListSO recipeListSO;

    private RecipeInformationUI recipeInformationUI;

    private List<GameObject> viewGameObjectList = new List<GameObject>();

    private ShowType showType = ShowType.Hide;

    enum ShowType
    {
        Show,
        Hide
    }

    private void Awake()
    {
        recipeInformationUI = rightWindowRect.gameObject.GetComponent<RecipeInformationUI>();
    }

    private void Start()
    {
        InventoryShow();
    }

    private void Update()
    {
        if (Keyboard.current.capsLockKey.wasPressedThisFrame)
        {
            showType = showType == ShowType.Show ? ShowType.Hide : ShowType.Show;
        }

        InventoryType();
    }

    private void InventoryType()
    {
        if (showType == ShowType.Show)
        {
            leftWindowRect.DOAnchorPos(new Vector2(-450, 0), 0.7f);
            rightWindowRect.DOAnchorPos(new Vector2(450, 0), 0.7f);
        }
        else if (showType == ShowType.Hide)
        {
            leftWindowRect.DOAnchorPos(new Vector2(-450, 1200), 0.7f);
            rightWindowRect.DOAnchorPos(new Vector2(450, 1200), 0.7f);
        }
    }

    private void InventoryShow()
    {
        foreach (RecipeSO recipe in recipeListSO.recipeList)
        {
            GameObject recipeImage = Instantiate(inventoryImagePrefab, leftWindowRect.gameObject.transform);
            RecipeImageUI recipeUI = recipeImage.GetComponent<RecipeImageUI>();

            recipeUI.Create(recipe);
            viewGameObjectList.Add(recipeImage);
        }
    }
}
