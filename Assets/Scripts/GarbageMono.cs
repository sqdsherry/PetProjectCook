using UnityEngine;

public sealed class GarbageMono : MonoBehaviour, IInteractable
{
    public bool CanInteract(PlayerInteraction player)
    {
        return player.HasItem;
    }

    public void Interact(PlayerInteraction player)
    {
        if (player.HasItem)
        {
            FoodItem item = player.HeldItem;
            player.Drop();

            Debug.Log($"Выбросил в мусорку {item.Type.DisplayName}");
        }
    }

    public string GetInteractionText()
    {
        return "Выбросить предмет";
    }
}