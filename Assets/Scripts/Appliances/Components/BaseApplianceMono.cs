using UnityEngine;
using Zenject;

public abstract class BaseApplianceMono : MonoBehaviour, IAppliance, IInteractable
{
    [Inject] protected readonly FoodItemWorldFactory FoodItemFactory;

    [SerializeField] protected Transform itemPlacePosition;

    protected FoodItem PlacedData { get; private set; }
    protected FoodItemWorld PlacedVisual { get; private set; }

    public bool IsOccupied => PlacedData != null;

    public virtual ICookingMethod Method => null;

    public void Tick(float deltaTime)
    {
        if (IsOccupied)
        {
            PlacedData?.Tick(deltaTime);
        }
    }

    public void Place(FoodItem item)
    {
        Debug.Log($"[Place] Пытаюсь положить {item.Type.name} в {gameObject.name}");
        if (IsOccupied)
        {
            Debug.LogWarning("ВНИМАНИЕ: Плита уже занята!");
            return;
        }

        PlacedData = item;
        Debug.Log($"[Place] PlacedData установлена в {PlacedData.Type.name}. IsOccupied теперь: {IsOccupied}");
        OnItemPlaced(item); 

        Vector3 spawnPosition = itemPlacePosition != null ? itemPlacePosition.position : transform.position + Vector3.up;
        PlacedVisual = FoodItemFactory.Create();
        PlacedVisual.transform.position = spawnPosition;
        PlacedVisual.transform.rotation = Quaternion.identity;
        PlacedVisual.transform.SetParent(transform);
        PlacedVisual.InitializeWithItem(item);
    }

    public FoodItem Remove()
    {
        Debug.Log("Attempting to remove item from appliance...");
        if (!IsOccupied) return null;

        FoodItem item = PlacedData;
        PlacedData = null;
        OnItemRemoved(item);

        if (PlacedVisual != null)
        {
            Destroy(PlacedVisual.gameObject);
            PlacedVisual = null;
        }

        return item;
    }

    public virtual bool CanInteract(PlayerInteraction player)
    {
        if (player == null) return false;
        return (IsOccupied && !player.HasItem) || (!IsOccupied && player.HasItem);
    }

    public virtual void Interact(PlayerInteraction player)
    {
        Debug.Log($"Player is interacting with {gameObject.name}... IsOccupied: {IsOccupied}, PlayerHasItem: {player.HasItem}");
        if (!CanInteract(player)) return;

        if (player.HasItem && !IsOccupied)
        {
            HandlePlacement(player);
        }
        else if (!player.HasItem && IsOccupied)
        {
            Debug.Log("Player is interacting to pick up item from appliance...");
            HandlePickup(player);
        }
    }

    protected virtual void HandlePlacement(PlayerInteraction player)
    {
        FoodItem item = player.HeldItem;
        player.Drop();
        Place(item); 
    }

    protected virtual void HandlePickup(PlayerInteraction player)
    {
        Debug.Log("Player is interacting");
        FoodItem item = Remove(); 
        player.PickUp(item);
    }

    protected virtual void OnItemPlaced(FoodItem item) {}

    protected virtual void OnItemRemoved(FoodItem item) 
    {
        Debug.Log($"Item removed: {item.Type.name}");
        item.ClearMethod();
    }

    public abstract string GetInteractionText();
}