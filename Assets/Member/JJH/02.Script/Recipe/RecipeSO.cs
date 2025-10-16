using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "Scriptable Objects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public IngredientSO[] ingredients;
    public RecipeTasteType recipeFoodTaste;
    public RecipeTextureType recipeFoodTexture;
}

public enum RecipeTasteType //¸À
{
    Salty, //Â§¸À
    Spicy, //¸Å¿î¸À
    Sweet, //´Ü¸À
    Sour, //½Å¸À
    Bitter, //¾´¸À
    Nutty //°í¼ÒÇÑ¸À
}

public enum RecipeTextureType //½Ä°¨
{
    None, //¾øÀ½
    Crispy, //¹Ù»è
    Cruncky, //¾Æ»è
    Chewy, //ÂÌ±ê
    Dry, //ÆÜÆÜ
    Soft, //ºÎµå·¯¿ò
    Hard, //µüµüÇÔ
    Umami, //°¨Ä¥¸À
    Perfact //È²È¦ÇÔ
}