public static class FoodStateExtensions
{
    public static string ToDisplayText(this IFoodState state)
    {
        return state switch
        {
            RawState => "Raw",
            CookingState => "Cooking",
            CookedState => "Ready",
            BurnedState => "Burnt",
            _ => "Unknown"
        };
    }
}