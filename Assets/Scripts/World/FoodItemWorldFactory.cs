using UnityEngine;
using Zenject;

public class FoodItemWorldFactory : PlaceholderFactory<FoodItemWorld> 
{
    public GameObject CreateVisual(GameObject prefab, Transform parent)
    {
        GameObject visual = Object.Instantiate(prefab, parent);
        visual.transform.localPosition = Vector3.zero; 
        visual.transform.localRotation = Quaternion.identity; 

        return visual;
    }
}