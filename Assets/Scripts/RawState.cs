public sealed class RawState : IFoodState
{
    public void Enter(FoodItem item) { }
    public void Tick(FoodItem item, float dt) { /* ждём ApplyMethod */ }
    public void Exit(FoodItem item) { }
}