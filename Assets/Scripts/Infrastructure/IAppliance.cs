public interface IAppliance
{
    ICookingMethod Method { get; }
    bool IsOccupied { get; }
    void Place(FoodItem item);
    FoodItem Remove();
    void Tick(float deltaTime);
}