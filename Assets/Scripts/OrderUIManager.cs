using System.Collections.Generic;
using UnityEngine;

public class OrderUIManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private OrderCardUI orderCardPrefab;

    private Dictionary<OrderInstance, OrderCardUI> cards = new();

/*    private void Start()
    {
        Debug.Log($"OrderManager.Instance = {OrderManager.Instance}");
        Debug.Log($"container = {container}");
        Debug.Log($"cardPrefab = {orderCardPrefab}");

        OrderManager.Instance.OnOrderSpawned += HandleOrderSpawned;
        OrderManager.Instance.OnOrderCompleted += HandleOrderCompleted;
    }*/

    private void OnEnable()
    {
        OrderManager.Instance.OnOrderSpawned += HandleOrderSpawned;
        OrderManager.Instance.OnOrderCompleted += HandleOrderCompleted;
    }
    private void OnDisable()
    {
        OrderManager.Instance.OnOrderSpawned -= HandleOrderSpawned;
        OrderManager.Instance.OnOrderCompleted -= HandleOrderCompleted;
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