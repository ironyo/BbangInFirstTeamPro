using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeInformationUI : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI ingredientText;
    [SerializeField] private TextMeshProUGUI tasteText;
    [SerializeField] private TextMeshProUGUI textureText;

    [Header("Image")]
    [SerializeField] private Image foodImage;

    public void ShowIngredientInformation(RecipeSO ingredient)
    {

    }

    public void NotShowIngredientInformation()
    {
        InformationActive(false);
    }

    private void InformationActive(bool value)
    {
        nameText.gameObject.SetActive(value);
        descriptionText.gameObject.SetActive(value);
        ingredientText.gameObject.SetActive(value);
        tasteText.gameObject.SetActive(value);
        textureText.gameObject.SetActive(value);
        foodImage.gameObject.SetActive(value);
    }
}
