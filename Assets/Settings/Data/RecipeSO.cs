using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Kitchen/Recipe")]
public class RecipeSO : ScriptableObject
{
    public string DishName;
    public List<IngredientRequirement> RequiredIngredients;
    public FoodTypeSO ResultDishType;
}