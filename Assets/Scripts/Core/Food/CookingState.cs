public sealed class CookingState : IFoodState
{
    public FoodStateType Type => FoodStateType.Cooking;
    public void Enter(FoodItem item) 
    {
        item.Progress.ResetForCooking(item.Type.BaseCookTime, item.Type.BurnTime);
    }

    public void Tick(FoodItem item, float dt)
    {         
        float speed = item.CurrentCookSpeed;
        if (speed <= 0f) return;
        item.Progress.Add(speed * dt);

        if (item.Progress.IsCooked && !(item.CurrentState is CookedState))
        {
            item.SetState(new CookedState());
        }
        else if (item.Progress.IsBurned && !(item.CurrentState is BurnedState))
        {
            item.SetState(new BurnedState());
        }
    }

    public void Exit(FoodItem item) { }
}