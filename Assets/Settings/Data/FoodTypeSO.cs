using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/Food Type")]
public class FoodTypeSO : ScriptableObject
{
    public FoodItemWorld visualPrefab;
    public string DisplayName;
    public float BaseCookTime;
    public float BurnTime;
}