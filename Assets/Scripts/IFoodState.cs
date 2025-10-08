public interface IFoodState
{
    public void Enter(FoodItem item);
    public void Tick(FoodItem item, float dt);
    public void Exit(FoodItem item);
}
