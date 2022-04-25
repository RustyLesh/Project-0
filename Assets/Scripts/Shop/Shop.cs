using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project0;

public class Shop : MonoBehaviour
{
    [SerializeField]Dictionary<int, ShopItem> shopInventory;

    public ShopItem GetShopItem(int slotNumber)
    {
        shopInventory.TryGetValue(slotNumber, out ShopItem returner);

        return returner;
    }

    public void PurchaseItem(Wallet wallet, int slotNumber)
    {
        shopInventory.TryGetValue(slotNumber, out ShopItem shopItem);

        if (shopItem.GetAmountInStock() > 0)
        {
            if (wallet.PayCoins(shopItem.GetPurchaseValue()))
            {
                shopItem.RemoveStock(1); //TODO: variable purchase amount, allow player to purchase multiple for bulk items.
            }
        }
        else
        {
            Debug.Log("Not enough in stock"); //TODO: Notify the player in ui.
        }
    }
}
