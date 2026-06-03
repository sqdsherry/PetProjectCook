using UnityEngine;

public class ItemDropper
{
    private readonly FoodItemWorldFactory _factory;

    public ItemDropper(FoodItemWorldFactory factory) => _factory = factory;

    public FoodItemWorld Drop(FoodItem item, Vector3 position)
    {
        var worldItem = _factory.Create(item.Type.visualPrefab);
        worldItem.transform.position = position;
        worldItem.InitializeWithItem(item);

        return worldItem;
    }
}