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

            if (item.CurrentStateType != RequiredState) {
                Debug.Log($"Состояние не совпадает, клиент не доволен");
                // - 50% к награде за заказ
                Debug.Log($"Проверяем заказ: Food={item.Type.DisplayName}, CurrentState={item.CurrentState}, RequiredState={RequiredState}");
            }
            return false;
        }
        else
            return true;

    }
}