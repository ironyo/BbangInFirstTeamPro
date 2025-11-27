using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class IngredientInventoryManager : MonoSingleton<IngredientInventoryManager>
{
    public Dictionary<IngredientSO, int> IngredientDictionary { get; private set; } = new Dictionary<IngredientSO, int>();

    public Action<IngredientSO, int> OnInventoryChanged;

    [ContextMenu("µñ¼Å³Ê¸® º¸±â")]
    private void PrintDictionary()
    {
        foreach (var value in IngredientDictionary)
            Debug.Log($"{value.Key} : {value.Value}");
    }


    public void AddInventoryIngredient(IngredientSO ingredient, int value)
    {
        if (IngredientDictionary.TryGetValue(ingredient, out int nowValue))
            IngredientDictionary[ingredient] = nowValue + value;
        else
            IngredientDictionary.Add(ingredient, value);

        OnInventoryChanged?.Invoke(ingredient, value);
    }
}
