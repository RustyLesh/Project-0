using System;
using UnityEngine;

[Serializable]
public class Old_ShopItem
{
    [SerializeField] int purchaseValue = 1;
    [SerializeField] int amountInStock = 0;
    [SerializeField] Item item;

    public Old_ShopItem(Item item, int purchaseValue, int amountInStock)
    {
        this.item = item;
        this.purchaseValue = purchaseValue;
        this.amountInStock = amountInStock;
    }

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
