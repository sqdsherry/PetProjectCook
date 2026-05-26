using UnityEngine;

[CreateAssetMenu(menuName = "Orders/Order")]
public class OrderSO : ScriptableObject
{
    public FoodTypeSO FoodType;
/*    public ICookingMethod RequiredMethod;*/
    public FoodStateType RequiredState;
    public string orderInfo;

    public bool IsSatisfiedBy(FoodItem item)
    {
        bool isTypeMatch = item.Type == FoodType;
        bool isStateMatch = item.CurrentStateType == RequiredState;

        if (!isTypeMatch)
        {
            Debug.Log($"Тип не совпадает: требуется {FoodType.DisplayName}");
            return false;
        }

        if (!isStateMatch)
        {
            Debug.Log($"Состояние не совпадает: требуется {RequiredState}, а еда {item.CurrentStateType}");
            return false;
        }

        return true;
    }
}