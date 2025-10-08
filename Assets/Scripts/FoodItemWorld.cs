using UnityEngine;

public class FoodItemWorld : MonoBehaviour, IHoldable, IInteractable
{
    [SerializeField] private FoodTypeSO foodType;
    [SerializeField] private Renderer itemRenderer;

    FoodItem foodItem;
    private bool isHeld = false;

    private void Start()
    {
        if (foodItem != null)
        {
            UpdateVisuals();
            return;
        }

        if (foodType != null)
        {
            foodItem = new FoodItem(foodType);
            UpdateVisuals();
            Debug.Log($"FoodItemWorld {gameObject.name} инициализирован с типом {foodType.DisplayName}");
        }
        else
            Debug.LogError($"FoodItemWorld {gameObject.name} - foodType не назначен!");

    }

    private void Update()
    {
        if (foodItem != null)
        {
            UpdateVisuals();
        }
    }

    public void InitializeWithItem(FoodItem item)
    {
        foodItem = item;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (foodItem == null || itemRenderer == null) return;
        itemRenderer.material.color = GetStateColor();
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
        if (CanInteract(player))
        {
            player.PickUp(this);
        }
    }

    public void SetHeld(bool isHeld)
    {
        this.isHeld = isHeld;
        gameObject.SetActive(!isHeld);
    }

    private Color GetStateColor()
    {
        if (foodItem == null) return Color.white;

        return foodItem.CurrentState switch
        {
            RawState => new Color(0.6f, 0.3f, 0.0f), // Brown color 
            CookingState => new Color(1.0f, 0.5f, 0.0f), // Orange color
            CookedState => Color.yellow,
            BurnedState => Color.black,
            _ => Color.white
        };
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
}