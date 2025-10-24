using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecipeIllustratedGuide : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;
    private RectTransform rect;

    private ShowType showType = ShowType.Hide;

    enum ShowType
    {
        Show,
        Hide
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
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
}
