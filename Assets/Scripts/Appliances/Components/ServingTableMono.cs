using UnityEngine;
using Zenject;

public sealed class ServingTableMono : BaseApplianceMono
{
    [Inject] private FoodItem.Factory _foodItemFactory;

    private RecipeMatcher _recipeMatcher;

    [Inject]
    public void Construct(RecipeMatcher recipeMatcher)
    {
        _recipeMatcher = recipeMatcher;
        Debug.Log("RecipeMatcher óñïåøíî ïðîèíæåêòèðîâàí â ServingTableMono!");
    }

    private void Update()
    {
        Tick(Time.deltaTime);
    }

    public override bool CanInteract(PlayerInteraction player)
    {
        if (player == null) return false;

        if (player.HasItem)
            return true;

        return IsOccupied && !player.HasItem;
    }

    public override void Interact(PlayerInteraction player)
    {
        if (player.HasItem)
        {
            if (!IsOccupied)
            {
                base.HandlePlacement(player);
            }
            else
            {
                FoodItem itemOnTable = PlacedData;

                itemOnTable.AddIngredient(player.HeldItem.Type);

                player.Drop();

                PlacedVisual.InitializeWithItem(itemOnTable);

                RecipeSO foundRecipe;

                if (_recipeMatcher == null)
                    Debug.LogError("ÊÐÈÒÈ×ÅÑÊÈ: _recipeMatcher == null!");
                if (PlacedData == null)
                    Debug.LogError("ÊÐÈÒÈ×ÅÑÊÈ: PlacedData (itemOnTable) == null!");
                else if (PlacedData.Ingredients == null)
                    Debug.LogError("ÊÐÈÒÈ×ÅÑÊÈ: PlacedData.Ingredients == null!");
                else
                    Debug.Log($"[Debug] Ìàò÷åð: {_recipeMatcher}, Èíãðåäèåíòîâ: {PlacedData.Ingredients.Count}");
                if (_recipeMatcher.IsRecipeComplete(itemOnTable.Ingredients, out foundRecipe))
                {
                    Assemble(foundRecipe);
                }
            }
        }
        else if (IsOccupied)
        {
            base.HandlePickup(player);
        }
    }

    private void Assemble(RecipeSO recipe)
    {
        Remove();

        FoodItem resultDish = _foodItemFactory.Create(recipe.ResultDishType);
        Place(resultDish);
    }

    protected override void OnItemPlaced(FoodItem item)
    {
        base.OnItemPlaced(item);
    }

    public override string GetInteractionText()
    {
        if (IsOccupied && PlacedData != null)
        {
            string stateText = GetStateText(PlacedData);
            return $"Take {PlacedData.Type.DisplayName} ({stateText})";
        }

        return "Put on the serving stove";
    }

    private string GetStateText(FoodItem item)
    {
        return item.CurrentState switch
        {
            RawState => "Raw",
            CookingState => "Coocking",
            CookedState => "Ready",
            BurnedState => "Burnt",
            _ => "unknown"
        };
    }
}