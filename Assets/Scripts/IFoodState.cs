public interface IFoodState
{
    FoodStateType Type { get; }
    public void Enter(FoodItem item);
    public void Tick(FoodItem item, float dt);
    public void Exit(FoodItem item);
}
