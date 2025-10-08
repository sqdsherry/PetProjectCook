public sealed class BurnedState : IFoodState
{
    public void Enter(FoodItem item) { /* отметить «сгорело» */ }
    public void Tick(FoodItem item, float dt) { /* пусто */ }
    public void Exit(FoodItem item) { }
}