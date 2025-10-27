using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecipeIllustratedGuideUI : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private GameObject recipeUIPrefab;

    private RectTransform rect;

    private ShowType showType = ShowType.Hide;

    private List<GameObject> viewGameObjectList = new List<GameObject>();
    private List<RecipeSO> viewList = new List<RecipeSO>();

    enum ShowType
    {
        Show,
        Hide
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        InventoryShow();

        foreach (RecipeSO item in recipeListSO.recipeList)
            viewList.Add(item);

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
            rect.DOAnchorPos(new Vector2(0, 0), 0.7f);
        }
        else if (showType == ShowType.Hide)
        {
            rect.DOAnchorPos(new Vector2(0, 1200), 0.7f);
        }
    }

    private void InventoryShow()
    {
        foreach (GameObject recipeImage in viewGameObjectList)
        {
            RecipeImageUI recipeImageUI = recipeImage.GetComponent<RecipeImageUI>();
        }
    }
}