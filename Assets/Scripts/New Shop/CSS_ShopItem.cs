/*
 * Author: Peter An
 * 
 * This class is for the UI to know if its a shop slot
 * rather than a inventory slot.
 * 
 */

using Project0.Inventories;
using UnityEngine;

namespace Project0.Shops
{
    public class CSS_ShopItem
    {
        SO_InventoryItem item;
        int availability;
        int price;
        int quantityInTransaction;

        public CSS_ShopItem(SO_InventoryItem item, int availability, int price, int quantityInTransaction)
        {
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }

        public SO_InventoryItem GetInventoryItem()
        {
            return item;
        }

        public Sprite GetIcon()
        {
            return item.GetIcon();
        }

        public string GetName()
        {
            return item.GetDisplayName();
        }

        public int GetAvailability()
        {
            return this.availability;
        }

        public int GetPrice()
        {
            return this.price;
        }

        public int GetQuantityInTransaction()
        {
            return this.quantityInTransaction;
        }
    }
}