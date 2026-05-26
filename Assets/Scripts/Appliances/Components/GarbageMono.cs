using UnityEngine;

public sealed class GarbageMono : MonoBehaviour, IInteractable
{
    public bool CanInteract(PlayerInteraction player)
    {
        if (player == null) return false;
        return player.HasItem;
    }

    public void Interact(PlayerInteraction player)
    {
        if (player.HasItem)
        {
            FoodItem item = player.HeldItem;
            player.Drop();

            Debug.Log($"Threw it in the trash {item.Type.DisplayName}");
        }
    }

    public string GetInteractionText()
    {
        return "Throw away an item";
    }
}