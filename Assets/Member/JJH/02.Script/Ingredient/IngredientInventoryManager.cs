using System.Collections.Generic;

public class IngredientInventoryManager : MonoSingleton<IngredientInventoryManager>
{
    public Dictionary<IngredientSO, int> ingredientDictionary { get; private set; } = new Dictionary<IngredientSO, int>();




    public void AddInventoryIngredient(IngredientSO ingredient, int value)
    {
        ingredientDictionary[ingredient] += value;
    }
}
