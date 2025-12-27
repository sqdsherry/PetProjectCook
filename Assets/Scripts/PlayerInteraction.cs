using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private LayerMask interactionLayer = -1;

    //IHoldable heldItem;
    private FoodItem heldItem;
    IInteractable currentTarget;

    public bool HasItem => heldItem != null;
    // public IHoldable HeldItem => heldItem;
    public FoodItem HeldItem => heldItem;

    private void Update()
    {
        FindInteractableTarget();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItemToGround();
        }
    }

    private void FindInteractableTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange, interactionLayer);

        IInteractable closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider col in colliders)
        {
            if (col.transform == transform) continue;

            IInteractable interactable = col.GetComponent<IInteractable>();
            if (interactable != null && interactable.CanInteract(this))
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        currentTarget = closestInteractable;
    }


    public void TryInteract()
    {
        if (currentTarget is MonoBehaviour mb) 
        {
            Debug.Log($"Взаимодействую с: {mb.gameObject.name} на позиции {mb.transform.position}");
            currentTarget.Interact(this);
        }
    }

    public void PickUp(FoodItemWorld itemWorld)
    {
        if (heldItem != null) return;
        heldItem = itemWorld.GetHeldItem();
        Destroy(itemWorld.gameObject);

        Debug.Log($"Поднят: {heldItem.Type.DisplayName}");
    }

    public void PickUp(FoodItem item)
    {
        if (heldItem != null) return;
        heldItem = item;

        Debug.Log($"Поднят: {heldItem.Type.DisplayName}");
    }

    public void Drop()
    {
        heldItem = null;
    }

    public void DropItemToGround()
    {
        if (heldItem == null) return;

        FoodItemWorld prefab = heldItem.Type.visualPrefab;
        if (prefab == null) return;

        var dropPosition = transform.position + transform.forward + Vector3.up * 0.5f;
        FoodItemWorld worldItem = Instantiate(prefab, dropPosition, Quaternion.identity);
        worldItem.InitializeWithItem(heldItem);
        heldItem = null;

        Debug.Log("Предмет выброшен на пол");
    }
}