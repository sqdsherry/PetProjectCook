using UnityEngine;
using Zenject;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private FoodTypeSO foodTypeToSpawn;
    [SerializeField] private Transform spawnPoint;

    private FoodItemWorldFactory _factory;

    [Inject]
    public void Construct(FoodItemWorldFactory factory)
    {
        _factory = factory;
    }

    private void Start()
    {
        SpawnFood();
    }

    private void SpawnFood()
    {
        if (foodTypeToSpawn == null) return;

        FoodItem newItem = new FoodItem(foodTypeToSpawn);
        newItem.SetState(new RawState());

        FoodItemWorld worldItem = _factory.Create();

        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        worldItem.transform.position = position;
        worldItem.transform.rotation = Quaternion.identity;

        worldItem.InitializeWithItem(newItem);
    }
}