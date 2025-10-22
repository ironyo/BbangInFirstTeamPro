using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "JJH_SO/IngredientSO")]
public class IngredientSO : ScriptableObject
{
    public string foodName;
    public FoodGroupType foodGroup;
    public FoodTasteType foodTaste;
    public FoodTextureType foodTextureType;
    public FoodRarityType foodRarityType;

    [Header("»óÁ¡¿ë")]
    public Sprite FoodSprite;
    public int Price;
    public int Stock;
    
}

public enum FoodGroupType //½ÄÀç·á±º
{
    Vegetablse, //¾ßÃ¤·ù
    Meat, //À°·ù
    Fish, //¾î·ù
    Fruit, //°úÀÏ·ù
    MSG //Çâ½Å·á
}

public enum FoodTasteType //¸À
{
    Salty, //Â§¸À
    Spicy, //¸Å¿î¸À
    Sweet, //´Ü¸À
    Sour, //½Å¸À
    Bitter, //¾´¸À
    Nutty //°í¼ÒÇÑ¸À
}

public enum FoodTextureType //½Ä°¨
{
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