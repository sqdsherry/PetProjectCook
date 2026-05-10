public class Table
{
    public string Id => "table";
    public string DisplayName => "Table";

    public void Start(FoodItem item)
    {

    }

    public void Stop(FoodItem item)
    {
        // выключить ефекты/звук
        // ничего не сбрасываем: прогресс/состо€ние управл€ютс€ в State/ FoodItem
    }
}