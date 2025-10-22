using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WantRecipeSOList", menuName = "H_SO/WantRecipeSOList")]
public class WantRecipeSOList : ScriptableObject
{
    public RecipeSOList[] recipeSOLists;
}
