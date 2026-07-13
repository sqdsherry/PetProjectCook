using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<FoodTypeSO> foodsTypeToSpawn;
    [SerializeField] private Transform spawnPoint;

    private FoodItemWorldFactory _worldFactory;
    private FoodItem.Factory _foodFactory;

    [Inject]
    public void Construct(FoodItemWorldFactory worldFactory, FoodItem.Factory foodFactory)
    {
        _worldFactory = worldFactory;
        _foodFactory = foodFactory;
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

        FoodItem newItem = _foodFactory.Create(spawnFoodItem); 
        FoodItemWorld worldItem = _worldFactory.Create(newItem.Type.visualPrefab);

        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        worldItem.transform.SetPositionAndRotation(position, Quaternion.identity);

        worldItem.InitializeWithItem(newItem);
    }
}