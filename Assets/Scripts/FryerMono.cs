using UnityEngine;

public sealed class FryerMono : MonoBehaviour, IAppliance, IInteractable
{
    [SerializeField] private FoodTypeSO debugType;
    [SerializeField] private FoodItemWorld foodItemPrefab;
    [SerializeField] private Transform itemPlacePosition;

    private FoodItem placed;
    private FoodItemWorld placedVisual;

    private readonly ICookingMethod method = new Stove();
    public ICookingMethod Method => method;
    public bool IsOccupied => placed != null;

    public void Place(FoodItem item)
    {
        if (IsOccupied) return;
        placed = item;
        placed.ApplyMethod(Method);
    }
    public FoodItem Remove()
    {
        if (!IsOccupied) return null;
        placed.ClearMethod();
        var outItem = placed;
        placed = null;
        return outItem;
    }

    public void Tick(float deltaTime)
    {
        if (IsOccupied) placed?.Tick(deltaTime);
    }

    private void Update()
    {
        Tick(Time.deltaTime);
    }


    // Временный дебаг
    private void Start()
    {
        if (debugType != null)
        {
            var item = new FoodItem(debugType);
            Place(item);
        }
    }

    public bool CanInteract(PlayerInteraction player)
    {
        return (player.HasItem && !IsOccupied) || (!player.HasItem && IsOccupied);
    }

    public void Interact(PlayerInteraction player)
    {
        // Игрок ставит предмет на плиту
        if (player.HasItem && !IsOccupied)
        {
            FoodItem item = player.HeldItem;
            player.Drop();
            Place(item);

            Vector3 spawnPosition = itemPlacePosition != null ? itemPlacePosition.position : transform.position + Vector3.up;
            placedVisual = Instantiate(foodItemPrefab, transform.position + Vector3.up, Quaternion.identity);
            placedVisual.InitializeWithItem(item);
            placedVisual.transform.SetParent(transform);

            Debug.Log($"Поставил {item.Type.DisplayName} на плиту");
        }
        // Игрок снимает предмет с плиты
        else if (!player.HasItem && IsOccupied)
        {
            FoodItem item = Remove();

            if (foodItemPrefab != null)
            {
                Destroy(placedVisual.gameObject);
                placedVisual = null;
            }

            player.PickUp(item);

            Debug.Log($"Взял {item.Type.DisplayName} с плиты");
        }
    }

    public string GetInteractionText()
    {
        if (IsOccupied)
        {
            string stateText = GetStateText(placed);
            return $"Взять {placed.Type.DisplayName} ({stateText})";
        }
        else
        {
            return "Поставить на плиту";
        }
    }

    private string GetStateText(FoodItem item)
    {
        return item.CurrentState switch
        {
            RawState => "сырой",
            CookingState => "готовится",
            CookedState => "готовый",
            BurnedState => "сгоревший",
            _ => "неизвестно"
        };
    }
}