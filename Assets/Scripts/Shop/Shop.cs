using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using Project0;

public class Shop : MonoBehaviour
{
    [SerializeField] List<ShopItem> shopInventory = new List<ShopItem>();

    Wallet wallet;

    public delegate void ShopChanged();
    public static event ShopChanged OnShopChanged;

    void Start()
    {
        wallet = FindObjectOfType<MoneyManager>().PlayerWallet;
    }

    public List<ShopItem> GetAllShotItem()
    {
        return shopInventory;
    }

    public ShopItem GetShopItem(int slotNumber)
    {
        return shopInventory[slotNumber];
    }

    public void PurchaseItem(int slotNumber)
    {

        if (shopInventory[slotNumber].GetAmountInStock() > 0)
        {
            if (wallet.PayCoins(shopInventory[slotNumber].GetPurchaseValue()))
            {
                shopInventory[slotNumber].RemoveStock(1); //TODO: variable purchase amount, allow player to purchase multiple for bulk items (if bulk items are added, eg bombs/nukes).
                OnShopChanged.Invoke();
            }
        }
        else
        {
            Debug.Log("Not enough in stock"); //TODO: Notify the player in ui.
        }
    }

    public int GetItemCount() { return shopInventory.Count; }

    public Item GetItemAtIndex(int index) { return shopInventory[index].GetItem(); }
}
