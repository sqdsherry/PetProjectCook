using UnityEngine;
using Zenject;

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

    private DiContainer _container;
    private ItemDropper _dropper;

    [Inject]
    public void Construct(DiContainer container, FoodItemWorldFactory factory)
    {
        _container = container;
        _dropper = new ItemDropper(factory);
    }

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
        Debug.Log($"Попытка взаимодействия... Текущий таргет: {currentTarget}");

        if (currentTarget == null)
        {
            Debug.LogWarning("currentTarget равен null! Проверь, как работает Raycast/Trigger.");
            return;
        }

        if (currentTarget is MonoBehaviour mb)
        {
            Debug.Log($"Взаимодействую с: {mb.gameObject.name}");
            currentTarget.Interact(this);
        }
        else
        {
            Debug.Log($"Объект {currentTarget.GetType()} не является MonoBehaviour!");
        }
    }

    public void PickUp(FoodItemWorld itemWorld)
    {
        if (heldItem != null) return;

        FoodItem item = itemWorld.GetHeldItem();
        if (item == null)
        {
            Debug.LogError("Ошибка: объект FoodItemWorld не инициализирован (foodItem == null)!");
            return;
        }

        heldItem = item;
        Destroy(itemWorld.gameObject);
        Debug.Log($"Поднят: {heldItem.Type.DisplayName}");
        currentTarget = null;
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

        Vector3 dropPosition = transform.position + transform.forward + Vector3.up * 0.5f;
        FoodItemWorld spawnedItem = _dropper.Drop(heldItem, dropPosition);

        heldItem = null;
    }
}