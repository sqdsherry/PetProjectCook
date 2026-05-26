using UnityEngine;

[CreateAssetMenu(fileName = "NewFoodVisuals", menuName = "Kitchen/Food Visuals")]
public sealed class FoodVisualsSO : ScriptableObject
{
	public GameObject rawPrefab;
	public GameObject cookedPrefab;
	public GameObject burnedPrefab;
}