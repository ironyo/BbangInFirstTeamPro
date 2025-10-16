using System.Collections.Generic;

public class IngredientInventoryManager : MonoSingleton<IngredientInventoryManager>
{
    public Dictionary<IngredientSO, int> ingredientDictionary { get; private set; } = new Dictionary<IngredientSO, int>();




    public void AddInventoryIngredient(IngredientSO ingredient, int value)
    {
        if (ingredientDictionary.TryGetValue(ingredient, out int nowValue))
            ingredientDictionary[ingredient] = nowValue + value;
        else
            ingredientDictionary.Add(ingredient, value);
    }
}
