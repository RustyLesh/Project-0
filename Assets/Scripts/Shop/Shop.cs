using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using Project0;

public class Shop : MonoBehaviour
{
    [SerializeField] List<Old_ShopItem> shopInventory = new List<Old_ShopItem>();

    CSS_MoneyManager moneyManager;
    OldInventory inventory;

    public delegate void ShopChanged();
    public static event ShopChanged OnShopChanged;

    void Start()
    {
        moneyManager = FindObjectOfType<CSS_MoneyManager>();
        inventory = OldInventory.instance;
    }

    public List<Old_ShopItem> GetAllShotItem()
    {
        return shopInventory;
    }

    public Old_ShopItem GetShopItem(int slotNumber)
    {
        return shopInventory[slotNumber];
    }

    public void PurchaseItem(int slotNumber)
    {

        if (shopInventory[slotNumber].GetAmountInStock() > 0)
        {
            if (moneyManager.PayCoins(shopInventory[slotNumber].GetPurchaseValue()))
            {
                inventory.Add(shopInventory[slotNumber].GetItem());
                shopInventory[slotNumber].RemoveStock(1); //TODO: variable purchase amount, allow player to purchase multiple for bulk items (if bulk items are added, eg bombs/nukes).
                OnShopChanged.Invoke();
            }
        }
        else
        {
            Debug.Log("Not enough in stock"); //TODO: Notify the player in ui.
        }
    }

    public void AddItemIntoShop(Item item, int amount, int price)
    {
        shopInventory.Add(new Old_ShopItem(item, amount, price));
        OnShopChanged.Invoke();
    }

    public void DeleteItemInInventory(int index)
    {
        shopInventory.RemoveAt(index);
        OnShopChanged.Invoke();
    }

    public int GetItemCount() { return shopInventory.Count; }

    public Old_ShopItem GetItemAtIndex(int index) { return shopInventory[index]; }//TODO: Change to ShopItem
}
