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
        Dictionary<InventoryItem, int> stock = new Dictionary<InventoryItem, int>();
        CSS_Shopper currentShopper = null;

        public event Action onChange;

        void Awake()
        {
            foreach(StockItemConfig config in stockConfig)
            {
                stock[config.item] = config.initialStock;
            }
        }

        public void SetShopper(CSS_Shopper shopper)
        {
            this.currentShopper = shopper;
        }

        public IEnumerable<CSS_ShopItem> GetFilteredItems()
        {
            return GetAllItems();
        }

        public IEnumerable<CSS_ShopItem> GetAllItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                int price = (int) (config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100));
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                int currentStock = stock[config.item];
                yield return new CSS_ShopItem(config.item, currentStock, price, quantityInTransaction);
            }
        }

        public void SelectFilter(ItemCategory category) { }

        public ItemCategory GetFilter() { return ItemCategory.None; }

        // FUTURE IMPLEMENTATION
        //public void SelectMode(bool isBuying) { }
        //public bool IsBuyingMode() { return true; }

        public bool CanTransact() 
        { 
            if(IsTransactionEmpty()) return false;
            if (!HasSufficientFunds()) return false;
            if (!HasInventorySpace()) return false;

            return true;
        }

        public void ConfirmTransaction() 
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            CSS_MoneyManager shopperPurse = currentShopper.GetPurse();

            if (shopperInventory == null ) return;

            foreach (CSS_ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                int price = shopItem.GetPrice();
                for(int i = 0; i < quantity; i++)
                {
                    // When player don't have enough money
                    if (shopperPurse.money < price) break;

                    bool success = shopperInventory.AddToFirstEmptySlot(item, 1);
                    if (success)
                    {
                        AddToTransaction(item, -1);
                        stock[item]--;
                        shopperPurse.PayCoins(price);
                    }
                }
            }

            if (onChange != null) onChange();
        }

        public float TransactionTotal() 
        { 
            float total = 0;

            foreach(CSS_ShopItem item in GetAllItems())
            {
                total += item.GetPrice() * item.GetQuantityInTransaction();
            }

            return total;
        }

        public void AddToTransaction(InventoryItem item, int quantity) 
        {

            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }

            if(transaction[item] + quantity > stock[item])
            {
                transaction[item] = stock[item];
            }
            else
            {
                transaction[item] += quantity;
            }

            if(transaction[item] <= 0)
            {
                transaction.Remove(item);
            }

            if (onChange != null)
            {
                onChange();
            }
        }
        public bool HasSufficientFunds()
        {
            CSS_MoneyManager purse = currentShopper.GetPurse();
            if (purse == null) return false;

            return purse.money > TransactionTotal();
        }

        public bool HasInventorySpace()
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            if (shopperInventory == null) return false;

            List<InventoryItem> flatItems = new List<InventoryItem>();
            foreach(CSS_ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                for(int i = 0; i < quantity; i++)
                {
                    flatItems.Add(item);
                }
            }

            return shopperInventory.HasSpaceFor(flatItems);
        }

        bool IsTransactionEmpty()
        {
            return transaction.Count == 0;
        }
    }
}