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
        if (IsOccupied || item == null) return;

        PlacedData = item;
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
        if (!CanInteract(player)) return;

        if (player.HasItem && !IsOccupied)
        {
            HandlePlacement(player);
        }
        else if (!player.HasItem && IsOccupied)
        {
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
        FoodItem item = Remove(); 
        player.PickUp(item);
    }

    protected virtual void OnItemPlaced(FoodItem item) {}

    protected virtual void OnItemRemoved(FoodItem item) {}

    public abstract string GetInteractionText();
}