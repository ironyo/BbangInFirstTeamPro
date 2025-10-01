using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "Scriptable Objects/IngredientSO")]
public class IngredientSO : ScriptableObject
{
    enum FoodGroupType
    {
        Vegetablse,
        Meat,
        Fish,
        Fruit,
        MSG
    }


}
