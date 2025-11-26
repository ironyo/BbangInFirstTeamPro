using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeImageUI : MonoBehaviour
{
    private RecipeInventoryUI recipeInventoryUI;

    private Image image;
    private Outline outline;

    private RecipeSO information;

    public void Create(RecipeSO recipe)
    {
        recipeInventoryUI = GetComponentInParent<RecipeInventoryUI>();
        outline = GetComponent<Outline>();
        image = GetComponent<Image>();

        outline.enabled = false;

        information = recipe;

        image.sprite = information.recipeImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObject);
        //recipeInventoryUI.ShowIngredientInformation(information);
    }

    public void OnSelect(BaseEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        outline.enabled = false;
        //recipeInventoryUI.HideIngredientInformation();
    }
}
