using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string foodName;
    private FoodGroupType foodGroup;
    private FoodTasteType[] foodTaste;
    private FoodTextureType foodTextureType;
    private FoodRarityType foodRarityType;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.gameObject.SetActive(true);
        text.text = $"{foodName}\n{foodGroup}\n{foodTaste}\n{foodTextureType}\n{foodRarityType}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.gameObject.SetActive(false);
    }

    public void Create(IngredientSO ingredient)
    {
        foodName = ingredient.foodName;
        foodGroup = ingredient.foodGroup;
        foodTaste = ingredient.foodTaste;
        foodTextureType = ingredient.foodTextureType;
        foodRarityType = ingredient.foodRarityType;
    }
}
