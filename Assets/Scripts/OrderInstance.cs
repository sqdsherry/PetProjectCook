using UnityEngine;

public class OrderInstance 
{
    public OrderSO Template { get; }

    public OrderInstance(OrderSO template)
    {
        Template = template;
    }

    public bool IsCompleted { get; private set; }

    public bool TryCompleteOrder(FoodItem item)
    {
        Debug.Log("OrderInstance: TryCompleteOrder called");

        return Template.IsSatisfiedBy(item);
    }
}