/*
 * Author: Peter An
 * 
 * The RowUI is the infomation from
 * each items from the shops.
 * This makes it easy to display each
 * components into the UI
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Project0.Shops;
using UnityEngine.UI;
using System;

namespace Project0.UI.Shops
{
    public class CSS_RowUI : MonoBehaviour
    {
        [SerializeField] Image iconField;
        [SerializeField] TMP_Text nameField;
        [SerializeField] TMP_Text availabilityField;
        [SerializeField] TMP_Text priceField;
        [SerializeField] TMP_Text quantityField;

        CSS_Shop currentShop = null;
        CSS_ShopItem item = null;

        public void Setup(CSS_Shop currentShop, CSS_ShopItem item)
        {
            this.currentShop = currentShop;
            this.item = item;

            iconField.sprite = item.GetIcon();
            nameField.text = item.GetName();
            availabilityField.text = $"{item.GetAvailability()}";
            priceField.text = $"${item.GetPrice()}";
            quantityField.text = $"{item.GetQuantityInTransaction()}";
        }

        public void Add()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), 1);
        }

        public void Remove()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), -1);
        }
    }
}