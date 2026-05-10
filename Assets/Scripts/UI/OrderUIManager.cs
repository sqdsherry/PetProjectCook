using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderUIManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private OrderCardUI orderCardPrefab;

    private OrderManager _orderManager;
    private Dictionary<OrderInstance, OrderCardUI> cards = new();

    [Inject]
    public void Construct(OrderManager orderManager)
    {
        _orderManager = orderManager;
    }

    private void OnEnable()
    {
        _orderManager.OnOrderSpawned += HandleOrderSpawned;
        _orderManager.OnOrderCompleted += HandleOrderCompleted;
    }

    private void OnDisable()
    {
        _orderManager.OnOrderSpawned -= HandleOrderSpawned;
        _orderManager.OnOrderCompleted -= HandleOrderCompleted;
    }

    private void HandleOrderSpawned(OrderInstance order)
    {
        int number = cards.Count + 1;

        var card = Instantiate(orderCardPrefab, container);
        card.BindOrder(order, number);

        cards.Add(order, card);
    }

    private void HandleOrderCompleted(OrderInstance order)
    {
        if (!cards.TryGetValue(order, out var card)) return;

        Destroy(card.gameObject);
        cards.Remove(order);
    }
}