using UnityEngine;

[CreateAssetMenu(menuName = "Orders/Order")]
public class OrderSO : ScriptableObject
{
    public FoodTypeSO FoodType;
    public ICookingMethod RequiredMethod;
    public IFoodState RequiredState;

    public bool IsSatisfiedBy(FoodItem item)
    {
        if (item.Type != FoodType) return false;
        if (item.CurrentMethod != RequiredMethod) return false;
        if (item.CurrentState != RequiredState) return false;
        return true;
    }
}