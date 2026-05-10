using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderCardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text orderNumberText;
    [SerializeField] private TMP_Text orderNameText;
    [SerializeField] private TMP_Text orderBurnTypeText;

    public void BindOrder(OrderInstance order, int orderNumber)
    {
        orderNumberText.text = $"{orderNumber}";
        orderNameText.text = order.Template.FoodType.DisplayName;
        orderBurnTypeText.text = order.Template.RequiredState.ToString();
    }
}
