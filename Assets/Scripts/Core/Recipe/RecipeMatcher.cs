using System.Collections.Generic;
using System.Linq;
using Zenject;
using UnityEngine;

public class RecipeMatcher
{
    private readonly RecipeDatabaseSO _recipeDatabase;

    [Inject]
    public RecipeMatcher(RecipeDatabaseSO recipeDatabase)
    {
        _recipeDatabase = recipeDatabase;
    }

    public RecipeSO GetMatchingRecipe(List<FoodItem> currentItems)
    {
        foreach (RecipeSO recipe in _recipeDatabase.AllRecipes)
        {
            if (IsPotentialMatch(currentItems, recipe))
            {
                return recipe;
            }
        }
        return null;
    }

    public bool CanAddItem(List<FoodItem> currentItems, FoodItem newItem)
    {
        List<FoodItem> potentileRecipe = new List<FoodItem>(currentItems);
        potentileRecipe.Add(newItem);

        foreach (RecipeSO recipe in _recipeDatabase.AllRecipes)
        {
            if (IsPotentialMatch(potentileRecipe, recipe))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsPotentialMatch(List<FoodItem> items, RecipeSO recipe)
    {
        if (items.Count != recipe.RequiredIngredients.Count) return false;
        foreach (IngredientRequirement requirement in recipe.RequiredIngredients)
        {
            bool foundMatch = false;
            foreach (FoodItem item in items)
            {
                if (item.Type == requirement.Type && item.CurrentStateType == requirement.State)
                {
                    foundMatch = true;
                    break;
                }
            }
            if (!foundMatch) return false;
        }
        return true;
    }

    public bool IsRecipeComplete(IReadOnlyList<FoodTypeSO> currentIngredients, out RecipeSO completedRecipe)
    {
        completedRecipe = null;

        // 1. Дебаг входных данных
        if (currentIngredients == null) { Debug.LogError("currentIngredients list is NULL!"); return false; }

        // 2. Дебаг базы
        if (_recipeDatabase == null || _recipeDatabase.AllRecipes == null) { Debug.LogError("Database is NULL!"); return false; }

        foreach (RecipeSO recipe in _recipeDatabase.AllRecipes)
        {
            // 3. Дебаг конкретного рецепта
            if (recipe == null) { Debug.LogWarning("Найден пустой рецепт в базе!"); continue; }
            if (recipe.RequiredIngredients == null) { Debug.LogWarning($"Рецепт {recipe.name} имеет NULL список RequiredIngredients!"); continue; }

            if (currentIngredients.Count == recipe.RequiredIngredients.Count)
            {
                if (CheckFullMatch(currentIngredients, recipe))
                {
                    completedRecipe = recipe;
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckFullMatch(IReadOnlyList<FoodTypeSO> currentIngredients, RecipeSO recipe)
    {
        if (currentIngredients.Count != recipe.RequiredIngredients.Count)
            return false;

        foreach (var req in recipe.RequiredIngredients)
        {
            if (!currentIngredients.Contains(req.Type)) return false;
        }

        return true;
    }
}