using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInformationUI : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private TextMeshProUGUI foodGroupText;
    [SerializeField] private TextMeshProUGUI tasteText;
    [SerializeField] private TextMeshProUGUI textureText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Image")]
    [SerializeField] private Image foodImage;

    private void Start()
    {
        InformationActive(false);
    }

    public void ShowIngredientInformation(IngredientSO ingredient)
    {
        InformationActive(true);

        //À½½Ä ÀÌ¹ÌÁö
        foodImage.sprite = ingredient.foodImage;

        //À½½Ä ÀÌ¸§ ÅØ½ºÆ®
        nameText.text = ingredient.foodName;

        //Èñ±Íµµ ÅØ½ºÆ®
        switch (ingredient.foodRarityType)
        {
            case FoodRarityType.Nomal:
                rarityText.color = Color.gray;
                rarityText.text = "ÀÏ¹Ý";
                break;
            case FoodRarityType.Rare:
                rarityText.color = Color.blue;
                rarityText.text = "Èñ±Í";
                break;
            case FoodRarityType.Epic:
                rarityText.color = new Color32(180, 85, 162, 255);
                rarityText.text = "¿¡ÇÈ";
                break;
            case FoodRarityType.Legendary:
                rarityText.color = Color.yellow;
                rarityText.text = "Àü¼³";
                break;
        }

        //½ÄÇ°±º ÅØ½ºÆ®
        switch (ingredient.foodGroup)
        {
            case FoodGroupType.Vegetable:
                foodGroupText.text = $"¾ßÃ¤";
                break;
            case FoodGroupType.Meat:
                foodGroupText.text = $"À°·ù";
                break;
            case FoodGroupType.Fish:
                foodGroupText.text = $"¾î·ù";
                break;
            case FoodGroupType.Fruit:
                foodGroupText.text = $"°úÀÏ";
                break;
            case FoodGroupType.MSG:
                foodGroupText.text = $"Çâ½Å·á";
                break;
            case FoodGroupType.Space:
                foodGroupText.text = $"¿Ü°è Àç·á";
                break;
        }

        //¸À ÅØ½ºÆ®
        tasteText.text = "";
        foreach (FoodTasteType taste in ingredient.foodTaste)
        {
            switch (taste)
            {
                case FoodTasteType.Salty:
                    if (!string.IsNullOrEmpty(tasteText.text))
                        tasteText.text += ", ";
                    tasteText.text += "Â§¸À";
                    break;
                case FoodTasteType.Spicy:
                    if (!string.IsNullOrEmpty(tasteText.text))
                        tasteText.text += ", ";
                    tasteText.text += "¸Å¿î¸À";
                    break;
                case FoodTasteType.Sweet:
                    if (!string.IsNullOrEmpty(tasteText.text))
                        tasteText.text += ", ";
                    tasteText.text += "´Ü¸À";
                    break;
                case FoodTasteType.Sour:
                    if (!string.IsNullOrEmpty(tasteText.text))
                        tasteText.text += ", ";
                    tasteText.text += "½Å¸À";
                    break;
                case FoodTasteType.Bitter:
                    if (!string.IsNullOrEmpty(tasteText.text))
                        tasteText.text += ", ";
                    tasteText.text += "¾´¸À";
                    break;
                case FoodTasteType.Nutty:
                    if (!string.IsNullOrEmpty(tasteText.text))
                        tasteText.text += ", ";
                    tasteText.text += "°í¼ÒÇÑ¸À";
                    break;
                case FoodTasteType.Umami:
                    if (!string.IsNullOrEmpty(tasteText.text))
                        tasteText.text += ", ";
                    tasteText.text += "°¨Ä¥¸À";
                    break;
                case FoodTasteType.Perfact:
                    if (!string.IsNullOrEmpty(tasteText.text))
                        tasteText.text += ", ";
                    tasteText.text += "È²È¦ÇÑ ¸À";
                    break;
            }
        }

        //½Ä°¨ ÅØ½ºÆ®
        switch (ingredient.foodTextureType)
        {
            case FoodTextureType.None:
                textureText.text = " ";
                break;
            case FoodTextureType.Crispy:
                textureText.text = "¹Ù»èÇÔ";
                break;
            case FoodTextureType.Cruncky:
                textureText.text = "¾Æ»èÇÔ";
                break;
            case FoodTextureType.Chewy:
                textureText.text = "ÂÌ±êÇÔ";
                break;
            case FoodTextureType.Dry:
                textureText.text = "ÆÜÆÜÇÔ";
                break;
            case FoodTextureType.Soft:
                textureText.text = "ºÎµå·¯¿ò";
                break;
            case FoodTextureType.Hard:
                textureText.text = "µüµüÇÔ";
                break;
        }

        //¼³¸í ÅØ½ºÆ®
        descriptionText.text = ingredient.foodDescription;
    }

    public void HideIngredientInformation()
    {
        InformationActive(false);
    }

    private void InformationActive(bool value)
    {
        nameText.gameObject.SetActive(value);
        rarityText.gameObject.SetActive(value);
        foodGroupText.gameObject.SetActive(value);
        tasteText.gameObject.SetActive(value);
        textureText.gameObject.SetActive(value);
        descriptionText.gameObject.SetActive(value);
        foodImage.gameObject.SetActive(value);
    }
}
