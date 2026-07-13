using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderUIManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private OrderCardUI orderCardPrefab;

    private OrderManager _orderManager;
    private Dictionary<OrderInstance, OrderCardUI> _cards = new();
    private int _totalOrdersSpawned;

    [Inject]
    public void Construct(OrderManager orderManager)
    {
        _orderManager = orderManager;

        _orderManager.OnOrderSpawned += HandleOrderSpawned;
        _orderManager.OnOrderCompleted += HandleOrderRemoved;
        _orderManager.OnOrderExpired += HandleOrderRemoved;
    }

    private void HandleOrderSpawned(OrderInstance order)
    {
        _totalOrdersSpawned++;

        var card = Instantiate(orderCardPrefab, container);
        card.BindOrder(order, _totalOrdersSpawned);

        _cards.Add(order, card);
    }

    private void HandleOrderRemoved(OrderInstance order)
    {
        if (!_cards.TryGetValue(order, out var card)) return;

        Destroy(card.gameObject);
        _cards.Remove(order);
    }

    private void OnDestroy()
    {
        if (_orderManager != null)
        {
            _orderManager.OnOrderSpawned -= HandleOrderSpawned;
            _orderManager.OnOrderCompleted -= HandleOrderRemoved;
            _orderManager.OnOrderExpired -= HandleOrderRemoved;
        }
    }
}