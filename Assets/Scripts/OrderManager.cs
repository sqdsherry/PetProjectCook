using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{ 
    public OrderSO[] possibleOrders;
    private List<OrderInstance> activeOrders = new();

    private void Start()
    {
        SpawnOrder();
    }

    public void SpawnOrder()
    {
        activeOrders.Add(new OrderInstance(possibleOrders[Random.Range(0, possibleOrders.Length)]));
    }

    public bool TryDeliver(FoodItem item)
    {
        foreach (var order in activeOrders)
        {
            if (order.TryCompleteOrder(item))
            {
                Debug.Log("Order completed!");
                return true;
            }
        }
        Debug.Log("No matching order found.");
        return false;
    }
}