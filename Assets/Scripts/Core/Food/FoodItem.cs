using UnityEngine;
using Zenject;

public sealed class FoodItem
{
    public class Factory : PlaceholderFactory<FoodTypeSO, FoodItem> { }
    public IFoodState CurrentState { get; private set; }  // <Ч текущее состо€ние (сырое, приготовленное, сгоревшее)
    public FoodStateType CurrentStateType => CurrentState.Type;

    public FoodTypeSO Type { get; }   // <Ч ссылка на ассет
    public ICookingMethod CurrentMethod { get; private set; }
    public CookingProgress Progress { get; } = new CookingProgress();
    public float CurrentCookSpeed => CurrentMethod?.GetCookSpeed(Type) ?? 0f;

    public FoodItem(FoodTypeSO type)
    {
        Type = type;
        SetState(new RawState());
    }

    public void SetState(IFoodState nextState)
    {
        if (nextState == null || ReferenceEquals(CurrentState, nextState)) return;
        Debug.Log($"State changed to {nextState.GetType().Name}");
        CurrentState?.Exit(this);
        CurrentState = nextState;
        CurrentState.Enter(this);
    }

    public void ResetToRaw()
    {
        CurrentMethod?.Stop(this);
        CurrentMethod = null;
        
        Progress.ResetForCooking(Type.BaseCookTime, Type.BurnTime);

        SetState(new RawState());
    }

    public void ClearMethod()
    {
        CurrentMethod?.Stop(this);
        CurrentMethod = null;
    }

    public void Tick(float dt) => CurrentState?.Tick(this, dt);

    public void ApplyMethod(ICookingMethod method)
    {
        if (!method.CanCook(Type)) return;
        CurrentMethod?.Stop(this);
        CurrentMethod = method;
        CurrentMethod.Start(this);

        if (CurrentState is RawState)
        {
            SetState(new CookingState());
        }
    }
}
public enum FoodStateType
{
    Raw,
    Cooking,
    Cooked,
    Burned
}