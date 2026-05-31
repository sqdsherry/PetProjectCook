using System;
using UnityEngine;

public class FoodVisualizer : MonoBehaviour
{
    [SerializeField] private FoodVisualsSO visuals;

    private GameObject _currentVisual;
    private FoodItemWorldFactory _factory;

    public void Initialize(FoodItemWorldFactory factory)
    {
        _factory = factory;
    }

    public void SetState(IFoodState state)
    {
        // 1. Выбираем, какой префаб нам нужен
        GameObject prefabToSpawn = state switch
        {
            RawState => visuals.rawPrefab,
            CookedState => visuals.cookedPrefab,
            BurnedState => visuals.burnedPrefab,
            CookingState => _currentVisual != null ? _currentVisual : visuals.rawPrefab, 
            _ => null
        };

        if (prefabToSpawn == null) return;

        if (_currentVisual != null && prefabToSpawn == _currentVisual) return;

        if (_currentVisual != null) Destroy(_currentVisual);
        _currentVisual = _factory.CreateVisual(prefabToSpawn, transform);
    }
}