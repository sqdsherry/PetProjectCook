public interface IInteractable
{
    bool CanInteract(PlayerInteraction player);
    public void Interact(PlayerInteraction player);
    string GetInteractionText();
}
