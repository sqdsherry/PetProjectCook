using UnityEngine;
using Zenject;

public class FoodItemWorldFactory : PlaceholderFactory<GameObject, FoodItemWorld>
{
    private readonly DiContainer _container;

    public FoodItemWorldFactory(DiContainer container)
    {
        _container = container;
    }

    public override FoodItemWorld Create(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("КРИТИЧЕСКИ: Фабрика получила NULL префаб!");
        }
        else
        {
            Debug.Log($"Фабрика получила префаб: {prefab.name}");
        }

        return _container.InstantiatePrefabForComponent<FoodItemWorld>(prefab);
    }

    public GameObject CreateVisual(GameObject prefab, Transform parent)
    {
        GameObject visual = Object.Instantiate(prefab, parent);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localRotation = Quaternion.identity;
        return visual;
    }
}