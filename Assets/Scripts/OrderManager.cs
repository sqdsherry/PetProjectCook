using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    [SerializeField] private List<OrderSO> availableOrders;
    [SerializeField] private List<OrderInstance> activeOrders = new();
    [SerializeField] private TMP_Text[] orderText;

    public event Action<OrderInstance> OnOrderSpawned;
    public event Action<OrderInstance> OnOrderCompleted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    public void ClearOrders()
    {
        activeOrders.Clear();
        foreach (var text in orderText)
        {
            text.text = "";
        }
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
                return true;
            }
        }
        Debug.Log("No matching order found.");
        return false;
    }
}