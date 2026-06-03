using UnityEngine;
using Zenject;

public sealed class TableMono : BaseApplianceMono
{
    public override bool CanInteract(PlayerInteraction player)
    {
        if (player == null) return false;
        return base.CanInteract(player);
    }

    public override string GetInteractionText()
    {
        if (IsOccupied && PlacedData != null)
        {
            string stateText = GetStateText(PlacedData);
            return $"Take {PlacedData.Type.DisplayName} ({stateText})";
        }

        return "Place on table";
    }

    private string GetStateText(FoodItem item)
    {
        if (item == null) return "unknown";

        return item.CurrentState switch
        {
            RawState => "Raw",
            CookingState => "Cooking",
            CookedState => "Cooked",
            BurnedState => "Burned",
            _ => "unknown"
        };
    }
}