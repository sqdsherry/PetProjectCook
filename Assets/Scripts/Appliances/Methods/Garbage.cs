public class Garbage
{
    public string Id => "garbage";
    public string DisplayName => "Garbage";

    public void Start(FoodItem item)
    {

    }

    public void Stop(FoodItem item)
    {
        // выключить ефекты/звук
        // ничего не сбрасываем: прогресс/состо€ние управл€ютс€ в State/ FoodItem
    }
}