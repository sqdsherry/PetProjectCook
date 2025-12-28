public sealed class CookedState : IFoodState
{
    public FoodStateType Type => FoodStateType.Cooked;
    public void Enter(FoodItem item) { /* сигнал/метка «готово» */ }
    public void Tick(FoodItem item, float dt) 
    { 
        if (item.CurrentMethod == null) return;
        item.Progress.Add(item.CurrentCookSpeed * dt);
        if (item.Progress.IsBurned)
        {
            item.SetState(new BurnedState());
        }
    }
    public void Exit(FoodItem item) { }
}