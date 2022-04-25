using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : Item
{
    [SerializeField]int purchaseValue = 1;
    [SerializeField]int amountInStock = 0;

    public int GetPurchaseValue()
    {
        return purchaseValue;
    }

    public int GetAmountInStock()
    {
        return amountInStock;
    }

    public void RemoveStock(int amountPurchased)
    {
        if(amountInStock > 0)
        {
            amountInStock -= amountPurchased;
        }
        else
            Debug.LogError("Not enough stock to remove, stock should be greater than 0 to purchase");
    }
}
