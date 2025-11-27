using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientImageUI : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    private IngredientInventoryUI ingredientInventoryUI;

    private Image image;
    private TextMeshProUGUI amountText;
    private Outline outline;

    private IngredientSO information;
    private int foodAmount = 1;

    public void Create(IngredientSO ingredient, int amount)
    {
        ingredientInventoryUI = GetComponentInParent<IngredientInventoryUI>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
        outline = GetComponent<Outline>();
        image = GetComponent<Image>();

        outline.enabled = false;

        information = ingredient;

        image.sprite = information.foodImage;
        foodAmount = amount;

        amountText.text = foodAmount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObject);
        ingredientInventoryUI.ShowIngredientInformation(information);
    }

    public void OnSelect(BaseEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        outline.enabled = false;
        ingredientInventoryUI.HideIngredientInformation();
    }
}
