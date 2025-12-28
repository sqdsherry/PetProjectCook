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
        Debug.Log($"Проверка предмета");

        if (item.Type != FoodType)
        {
            Debug.Log($"Тип не совпадает. Требуется {FoodType.DisplayName}, а получен {item.Type.DisplayName}");
            return false;
        }
/*        if (item.CurrentMethod != RequiredMethod)
        {
            Debug.Log($"Метод не совпадает. Требуется {RequiredMethod}, а получен {item.CurrentMethod}");
            return false;
        }*/
        if (item.CurrentStateType != RequiredState)
        {
            Debug.Log($"Состояние не совпадает");
            Debug.Log($"Проверяем заказ: Food={item.Type.DisplayName}, CurrentState={item.CurrentState}, RequiredState={RequiredState}");
            return false;
        }
        return true;
    }
}