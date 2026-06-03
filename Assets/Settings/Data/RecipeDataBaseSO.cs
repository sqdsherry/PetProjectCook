using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "Kitchen/Recipe Database")]
public class RecipeDatabaseSO : ScriptableObject
{
    [SerializeField] private List<RecipeSO> allRecipes;

    public IReadOnlyList<RecipeSO> AllRecipes => allRecipes;
}