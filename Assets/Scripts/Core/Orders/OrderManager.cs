using System.Collections.Generic;
using UnityEngine;
using System;

[DefaultExecutionOrder(-100)]
public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<OrderSO> availableOrders;
    private List<OrderInstance> activeOrders = new();

    public event Action<OrderInstance> OnOrderSpawned;
    public event Action<OrderInstance> OnOrderCompleted;

    private void Start()
    {
        SpawnOrder();
    }

    public void SpawnOrder()
    {
        OrderSO orderSO = availableOrders[UnityEngine.Random.Range(0, activeOrders.Count)];

        OrderInstance instance = new OrderInstance(orderSO);
        activeOrders.Add(instance);

        Debug.Log($"New order: {instance.Template.name}");

        OnOrderSpawned?.Invoke(instance);
    }

    public void CompleteOrder(OrderInstance order)
    {
        activeOrders.Remove(order);
        OnOrderCompleted?.Invoke(order);
    }

    public bool TryDeliver(FoodItem item)
    {
        Debug.Log($"ORDER MANAGER: TryDeliver called, activeOrders={activeOrders.Count}");

        if (item == null)
            return false;

        foreach (var order in activeOrders)
        {
            if (order.TryCompleteOrder(item))
            {
                Debug.Log("Order completed!");
                CompleteOrder(order);
                return true;
            }
        }
        Debug.Log("No matching order found.");
        return false;
    }
}