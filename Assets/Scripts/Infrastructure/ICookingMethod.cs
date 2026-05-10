public interface ICookingMethod
{
    string Id { get; }
    string DisplayName { get; }
    bool CanCook(FoodTypeSO type);
    float GetCookSpeed(FoodTypeSO type);
    void Start(FoodItem item);
    void Stop(FoodItem item);
}
