using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecipeIllustratedGuideUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private GameObject recipeUIPrefab;

    [Header("Objects")]
    [SerializeField] private RectTransform illustratedGuideRect;
    [SerializeField] private RectTransform descriptionRect;

    private RecipeInformationUI recipeInformationUI;

    private ShowType showType = ShowType.Hide;

    private List<GameObject> viewGameObjectList = new List<GameObject>();
    private List<RecipeSO> viewRecipeSOList = new List<RecipeSO>();

    enum ShowType
    {
        Show,
        Hide
    }

    private void Awake()
    {
        StartInventoryShow();

        foreach (RecipeSO item in recipeListSO.recipeList)
            viewRecipeSOList.Add(item);
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
            illustratedGuideRect.DOAnchorPos(new Vector2(-450, 0), 0.7f);
            descriptionRect.DOAnchorPos(new Vector2(450, 0), 0.7f);
        }
        else if (showType == ShowType.Hide)
        {
            illustratedGuideRect.DOAnchorPos(new Vector2(-450, 1200), 0.7f);
            descriptionRect.DOAnchorPos(new Vector2(450, 1200), 0.7f);
        }
    }

    private void StartInventoryShow()
    {
        foreach (RecipeSO recipeSO in viewRecipeSOList)
        {
            GameObject recipeImageUIPrefab = Instantiate(recipeUIPrefab, illustratedGuideRect.gameObject.transform);
            viewGameObjectList.Add(recipeImageUIPrefab);
            RecipeImageUI recipeImageUI = recipeImageUIPrefab.GetComponent<RecipeImageUI>();
            recipeImageUI.Create(recipeSO);
        }
    }

    public void ShowIngredientInformation(RecipeSO ingredient)
    {
        recipeInformationUI.ShowIngredientInformation(ingredient);
    }

    public void NotShowIngredientInformation()
    {
        recipeInformationUI.NotShowIngredientInformation();
    }
}