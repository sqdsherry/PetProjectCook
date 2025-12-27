using UnityEngine;

public class DeliveryPointMono : MonoBehaviour, IInteractable
{
    public OrderManager orderManager;

    public bool CanInteract(PlayerInteraction player)
    {
        return player.HasItem;
    }

    public string GetInteractionText()
    {
        return "Поставить на стол заказов";
    }

    public void Interact(PlayerInteraction player)
    {
        if (orderManager.TryDeliver(player.HeldItem))
            player.Drop();
    }
}