using UnityEngine;
using Zenject;

public sealed class DeliveryPointMono : MonoBehaviour, IInteractable
{
    private OrderManager _orderManager;

    [Inject] 
    public void Construct(OrderManager orderManager)
    {
        _orderManager = orderManager;
    }

    public bool CanInteract(PlayerInteraction player)
    {
        if (player == null) return false;
        return player.HasItem;
    }


    public void Interact(PlayerInteraction player)
    {
        if (!player.HasItem) return;

        if (_orderManager.TryDeliver(player.HeldItem))
            player.Drop();
    }

    public string GetInteractionText()
    {
        return "Put on the delivery table";
    }
}