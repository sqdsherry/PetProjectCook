using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<FoodTypeSO> foodsTypeToSpawn;
    [SerializeField] private Transform spawnPoint;

    private FoodItemWorldFactory _factory;

    [Inject]
    public void Construct(FoodItemWorldFactory factory)
    {
        _factory = factory;
    }

    private void Start()
    {
        foreach (var foodType in foodsTypeToSpawn)
        {
            SpawnFood(foodType);
        }
    }

    private void SpawnFood(FoodTypeSO spawnFoodItem)
    {
        if (spawnFoodItem == null) return;

        FoodItem newItem = new FoodItem(spawnFoodItem);
        newItem.SetState(new RawState());

        FoodItemWorld worldItem = _factory.Create(newItem.Type.visualPrefab);

        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        worldItem.transform.position = position;
        worldItem.transform.rotation = Quaternion.identity;

        worldItem.InitializeWithItem(newItem);
    }
}