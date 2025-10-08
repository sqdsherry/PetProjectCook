public class Stove : ICookingMethod
{
    private const float CookSpeed = 1.0f;
    public string Id => "stove";
    public string DisplayName => "Stove";
    public bool CanCook(FoodTypeSO type) => type != null;
    public float GetCookSpeed(FoodTypeSO type) => CookSpeed;


    public void Start(FoodItem item)
    {

    }

    public void Stop(FoodItem item)
    {
        // выключить ефекты/звук
        // ничего не сбрасываем: прогресс/состо€ние управл€ютс€ в State/ FoodItem
    }
}