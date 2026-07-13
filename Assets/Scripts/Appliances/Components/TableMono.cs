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
            return $"Take {PlacedData.Type.DisplayName} ({PlacedData.CurrentState.ToDisplayText()})";
        }

        return "Put on the stove";
    }
}