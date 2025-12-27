public class OrderInstance 
{
    private OrderSO orderSO;

    public OrderInstance(OrderSO orderSO)
    {
        this.orderSO = orderSO;
    }

    public OrderSO Template { get; }
    public bool IsCompleted { get; private set; }

    public bool TryCompleteOrder(FoodItem item)
    {
        if (IsCompleted) return false;
        if (!Template.IsSatisfiedBy(item)) return false;
        IsCompleted = true;
        return true;
    }
}