using System;

public class CookingProgress
{
    public float Current { get; internal set; }
    public float ToCook { get; internal set; }
    public float ToBurn { get; internal set; }

    internal void ResetForCooking(float baseCookTime, float burnTime)
    {
        Current = 0f;
        ToCook = (baseCookTime);
        ToBurn = burnTime;
    }

    internal void Add(float value) { Current += value; }
    public bool IsCooked => Current >= ToCook && Current < ToBurn;
    public bool IsBurned => Current >= ToBurn;
}