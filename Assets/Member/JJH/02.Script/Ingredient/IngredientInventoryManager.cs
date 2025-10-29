using System;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInventoryManager : MonoSingleton<IngredientInventoryManager>
{
    public Dictionary<IngredientSO, int> ingredientDictionary { get; private set; } = new Dictionary<IngredientSO, int>();

    [ContextMenu("µñ¼Å³Ê¸® º¸±â")]
    private void PrintDictionary()
    {
        foreach (var value in ingredientDictionary)
            Debug.Log($"{value.Key} : {value.Value}");
    }

    public Action OnInventoryChanged;

    public void AddInventoryIngredient(IngredientSO ingredient, int value)
    {
        if (ingredientDictionary.TryGetValue(ingredient, out int nowValue))
            ingredientDictionary[ingredient] = nowValue + value;
        else
            ingredientDictionary.Add(ingredient, value);

        OnInventoryChanged?.Invoke();
    }
}
