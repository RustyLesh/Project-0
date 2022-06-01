using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Project0.Inventories;

namespace Project0.Shops
{
    public class CSS_Shop : MonoBehaviour
    {
        // Stock Config
        [SerializeField] StockItemConfig[] stockConfig;

        [System.Serializable]
        class StockItemConfig
        {
            public InventoryItem item;
            public int initialStock;
            [Range(0, 100)]
            public float buyingDiscountPercentage;
        }

        Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
        CSS_Shopper currentShopper = null;

        public event Action onChange;

        public void SetShopper(CSS_Shopper shopper)
        {
            this.currentShopper = shopper;
        }

        public IEnumerable<CSS_ShopItem> GetFilteredItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                yield return new CSS_ShopItem(config.item, config.initialStock, price, quantityInTransaction);
            }
        }

        public void SelectFilter(ItemCategory category) { }

        public ItemCategory GetFilter() { return ItemCategory.None; }

        public void SelectMode(bool isBuying) { }

        public bool IsBuyingMode() { return true; }

        public bool CanTransact() { return true; }

        public void ConfirmTransaction() 
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            if (shopperInventory == null) return;

            var transactionSnapshot = new Dictionary<InventoryItem, int>(transaction);
            foreach (InventoryItem item in transactionSnapshot.Keys)
            {
                int quantity = transaction[item];
                for(int i = 0; i < quantity; i++)
                {
                    bool success = shopperInventory.AddToFirstEmptySlot(item, 1);
                    if (success)
                    {
                        AddToTransaction(item, -1);
                    }
                }
            }
        }

        public float TransactionTotal() { return 0; }

        public void AddToTransaction(InventoryItem item, int quantity) 
        {

            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }

            transaction[item] += quantity;

            if(transaction[item] <= 0)
            {
                transaction.Remove(item);
            }

            if (onChange != null)
            {
                onChange();
            }
        }
    }
}