using UnityEngine;
using Zenject;

public class FoodItemWorld : MonoBehaviour, IHoldable, IInteractable
{
    [SerializeField] private FoodTypeSO foodType;

    FoodItem foodItem;
    private bool isHeld = false;

    private FoodVisualizer _visualizer;
    private FoodItemWorldFactory _factory;

    [Inject]
    public void Construct(FoodItemWorldFactory factory)
    {
        _factory = factory;
        _visualizer = GetComponent<FoodVisualizer>(); 
    }

    public void InitializeWithItem(FoodItem item)
    {
        foodItem = item;

        _visualizer.Initialize(_factory);
        item.OnStateChanged += _visualizer.SetState;
        _visualizer.SetState(foodItem.CurrentState);
    }

    public bool CanInteract(PlayerInteraction player)
    {
        return !player.HasItem;
    }

    public FoodItem GetHeldItem()
    {
        return foodItem;
    }

    public string GetInteractionText()
    {
        if (foodItem == null) return "Неизвестный предмет";

        string stateText = GetStateText();
        return $"Взять {foodItem.Type.DisplayName} ({stateText})";
    }

    public void Interact(PlayerInteraction player)
    {
        IAppliance appliance = GetComponentInParent<IAppliance>();

        if (appliance != null)
        {
            appliance.Interact(player);
        }
        else if (CanInteract(player))
        {
            player.PickUp(this);
        }
    }

    public void SetHeld(bool isHeld)
    {
        this.isHeld = isHeld;
        gameObject.SetActive(!isHeld);
    }

    private string GetStateText()
    {
        if (foodItem == null) return "неизвестно";

        return foodItem.CurrentState switch
        {
            RawState => "сырой",
            CookingState => "готовится",
            CookedState => "готовый",
            BurnedState => "сгоревший",
            _ => "неизвестно"
        };
    }

    private void OnDestroy()
    {
        if (foodItem != null)
        {
            foodItem.OnStateChanged -= _visualizer.SetState;
        }
    }
}