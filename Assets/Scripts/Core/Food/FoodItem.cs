using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class FoodItem
{
    public class Factory : PlaceholderFactory<FoodTypeSO, FoodItem> { }

    private IFoodState _currentState;
    public IFoodState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            OnStateChanged?.Invoke(_currentState); 
        }
    }
    public FoodStateType CurrentStateType => CurrentState.Type;

    public FoodTypeSO Type { get; }   
    public ICookingMethod CurrentMethod { get; private set; }
    public CookingProgress Progress { get; } = new CookingProgress();
    public float CurrentCookSpeed => CurrentMethod?.GetCookSpeed(Type) ?? 0f;

    public event Action<IFoodState> OnStateChanged;

    private List<FoodTypeSO> ingredients;

    public IReadOnlyList<FoodTypeSO> Ingredients => ingredients ?? new List<FoodTypeSO>();

    public FoodItem(FoodTypeSO type)
    {
        Type = type;
        ingredients = new List<FoodTypeSO> { type };
        SetState(new RawState());
    }

    public void AddIngredient(FoodTypeSO ingredientType)
    {
        ingredients ??= new List<FoodTypeSO>();
        ingredients.Add(ingredientType);
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

    public void Tick(float dt)
    {
        if (CurrentState is CookingState && CurrentMethod == null)
        {
            return;
        }

        CurrentState?.Tick(this, dt);
    }

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