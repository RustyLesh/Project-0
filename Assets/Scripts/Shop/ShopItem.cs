using System;
using UnityEngine;

[Serializable]
public class ShopItem
{
    [SerializeField]int purchaseValue = 1;
    [SerializeField]int amountInStock = 0;
    [SerializeField] Item item;
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

    public Item GetItem()
    {
        return item;
    }
}
