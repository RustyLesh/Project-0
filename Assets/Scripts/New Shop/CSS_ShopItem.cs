using Project0.Inventories;
using UnityEngine;

namespace Project0.Shops
{
    public class CSS_ShopItem
    {
        InventoryItem item;
        int availability;
        float price;
        int quantityInTransaction;

        public CSS_ShopItem(InventoryItem item, int availability, float price, int quantityInTransaction)
        {
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }

        public InventoryItem GetInventoryItem()
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

        public float GetPrice()
        {
            return this.price;
        }

        public int GetQuantityInTransaction()
        {
            return this.quantityInTransaction;
        }
    }
}