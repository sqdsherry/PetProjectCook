public class Fryer : ICookingMethod
{
    private const float CookSpeed = 1.5f;
    public string Id => "fryer";
    public string DisplayName => "Fryer";
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