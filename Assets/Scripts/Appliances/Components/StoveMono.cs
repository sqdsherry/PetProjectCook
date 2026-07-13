using UnityEngine;

public sealed class StoveMono : BaseApplianceMono
{
    [SerializeField] private FoodTypeSO cookType;

    private readonly ICookingMethod method = new Stove();
    public override ICookingMethod Method => method;

    private void Update()
    {
        Tick(Time.deltaTime);
    }

    public override bool CanInteract(PlayerInteraction player)
    {
        if (player == null) return false;

        if (player.HasItem && !IsOccupied)
            return player.HeldItem.Type == cookType;

        if (IsOccupied && !player.HasItem)
            return true;

        return false;
    }

    protected override void OnItemPlaced(FoodItem item)
    {
        base.OnItemPlaced(item);

        item.ApplyMethod(Method);
    }

    public override string GetInteractionText()
    {
        if (IsOccupied && PlacedData != null)
        {
            return $"Take {PlacedData.Type.DisplayName} ({PlacedData.CurrentState.ToDisplayText()})";
        }

        return "Put on the stove";
    }
}