using UnityEngine;
using Zenject; 

public class DeliveryPointMono : MonoBehaviour, IInteractable
{
    private OrderManager _orderManager;

    [Inject] 
    public void Construct(OrderManager orderManager)
    {
        _orderManager = orderManager;
    }

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
        if (!player.HasItem) return;  

        if (_orderManager.TryDeliver(player.HeldItem))
            player.Drop();
    }
}