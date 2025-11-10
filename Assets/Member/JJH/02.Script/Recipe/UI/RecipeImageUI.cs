using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeImageUI : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    private Image image;
    private GameObject describeObject;
    private Outline outline;

    private RecipeIllustratedGuideUI recipeIllustratedGuideUI;

    private RecipeSO information;

    public void Create(RecipeSO recipe)
    {
        outline = GetComponent<Outline>();
        image = GetComponent<Image>();
        recipeIllustratedGuideUI = GetComponent<RecipeIllustratedGuideUI>();

        outline.enabled = false;

        information = recipe;
        image.sprite = information.recipeImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObject);

        recipeIllustratedGuideUI.ShowIngredientInformation(information);
    }

    public void OnSelect(BaseEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        outline.enabled = false;
        recipeIllustratedGuideUI.NotShowIngredientInformation();
    }
}
