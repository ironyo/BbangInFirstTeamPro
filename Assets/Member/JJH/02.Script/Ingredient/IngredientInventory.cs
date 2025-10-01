using System.Collections.Generic;

public class IngredientInventory : MonoSingleton<IngredientInventory>
{
    public Dictionary<IngredientSO, int> ingredientDictionary { get; private set; } = new Dictionary<IngredientSO, int>();

    public void AddInventoryIngredient(IngredientSO ingredient, int value)
    {
        ingredientDictionary[ingredient] += value;
    }
}
