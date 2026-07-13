using UnityEngine;
using Zenject;

public sealed class FryerMono : BaseApplianceMono
{
    [SerializeField] private FoodTypeSO cookType;

    private readonly ICookingMethod method = new Fryer();
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

    public override string GetInteractionText()
    {
        if (IsOccupied && PlacedData != null)
        {
            return $"Take {PlacedData.Type.DisplayName} ({PlacedData.CurrentState.ToDisplayText()})";
        }

        return "Put on the stove";
    }
}