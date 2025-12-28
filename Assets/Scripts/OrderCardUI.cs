using TMPro;
using UnityEngine;

public class OrderCardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text orderNumberText;
    [SerializeField] private TMP_Text orderInfoText;

    private OrderInstance boundOrder;

    public void BindOrder(OrderInstance order, int orderNumber)
    {
        boundOrder = order;
        orderNumberText.text = $"Order {orderNumber + 1}";
        orderInfoText.text = order.Template.orderInfo;
    }
}
