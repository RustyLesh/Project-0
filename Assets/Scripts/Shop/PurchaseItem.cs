using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{
    OldInventory inventory;
    Shop shop;
    private void Start()
    {
        inventory = OldInventory.instance;
        shop = FindObjectOfType<Shop>();
    }

    public void Purchase(int index)
    {
        shop.GetShopItem(index).RemoveStock(1);
        inventory.Add(shop.GetShopItem(index).GetItem());
    }
}
