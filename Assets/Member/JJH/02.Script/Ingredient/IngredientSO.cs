using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "JJH_SO/IngredientSO")]
public class IngredientSO : ScriptableObject
{
    [Header("»óÁ¡¿ë")]
    public Sprite FoodSprite;
    public int Price;
    public int Stock;

    public string foodName; //À½½Ä ÀÌ¸§
    public string foodDescription; //À½½Ä ¼³¸í
    public FoodGroupType foodGroup; //À½½Ä ½ÄÀç·á±º
    public FoodTasteType[] foodTaste; //À½½Ä ¸À
    public FoodTextureType foodTextureType; //À½½Ä ½Ä°¨
    public FoodRarityType foodRarityType; //À½½Ä Èñ±Íµµ
    public Sprite foodImage;
}

public enum FoodGroupType //½ÄÀç·á±º
{
    Vegetable, //¾ßÃ¤·ù
    Meat, //À°·ù
    Fish, //¾î·ù
    Fruit, //°úÀÏ·ù
    MSG, //Çâ½Å·á
    Space //¿Ü°è
}

public enum FoodTasteType //¸À
{
    Salty, //Â§¸À
    Spicy, //¸Å¿î¸À
    Sweet, //´Ü¸À
    Sour, //½Å¸À
    Bitter, //¾´¸À
    Nutty, //°í¼ÒÇÑ¸À
    Umami, //°¨Ä¥¸À
    Perfact //È²È¦ÇÔ
}

public enum FoodTextureType //½Ä°¨
{
    None, //¾øÀ½
    Crispy, //¹Ù»è
    Cruncky, //¾Æ»è
    Chewy, //ÂÌ±ê
    Dry, //ÆÜÆÜ
    Soft, //ºÎµå·¯¿ò
    Hard //µüµüÇÔ
}

public enum FoodRarityType
{
    Nomal, //ÀÏ¹Ý
    Rare, //Èñ±Í
    Epic, //¿¡ÇÈ
    Legendary //Àü¼³
}