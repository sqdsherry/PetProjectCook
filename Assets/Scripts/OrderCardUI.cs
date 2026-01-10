using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderCardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text orderNumberText;
    [SerializeField] private TMP_Text orderInfoText;

    private OrderInstance boundOrder;

    public void BindOrder(OrderInstance order, int orderNumber)
    {
        orderNumberText.text = $"{orderNumber}";
        orderInfoText.text = order.Template.FoodType.DisplayName;
    }
}
