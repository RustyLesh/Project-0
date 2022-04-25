using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using Project0;
public class Shop : MonoBehaviour
{
    [SerializeField]List<ShopItem> shopInventory;

    public ShopItem GetShopItem(int slotNumber)
    {
        return shopInventory[slotNumber];
    }

    public void PurchaseItem(Wallet wallet, int slotNumber)
    {

        if (shopInventory[slotNumber].GetAmountInStock() > 0)
        {
            if (wallet.PayCoins(shopInventory[slotNumber].GetPurchaseValue()))
            {
                shopInventory[slotNumber].RemoveStock(1); //TODO: variable purchase amount, allow player to purchase multiple for bulk items.
            }
        }
        else
        {
            Debug.Log("Not enough in stock"); //TODO: Notify the player in ui.
        }
    }
}
