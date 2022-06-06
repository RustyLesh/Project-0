/*
 * Author: Peter An
 * 
 * This class manages the components in the
 * Shop UI so that the current shop can display each items
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Project0.Shops;

using TMPro;
using UnityEngine.UI;

namespace Project0.UI.Shops
{
    public class CSS_ShopUI : MonoBehaviour
    {
        CSS_Shopper shopper = null;
        [SerializeField] CSS_Shop currentShop = null;

        [SerializeField] Transform listRoot;
        [SerializeField] CSS_RowUI rowPrefab;
        [SerializeField] TMP_Text totalField;
        [SerializeField] Button confirmButton;

        Color originalTotalTextColor;

        void Start()
        {
            originalTotalTextColor = totalField.color;

            shopper = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<CSS_Shopper>();

            if (shopper == null) return;
            shopper.SetActiveShop(currentShop);
            confirmButton.onClick.AddListener(ConfirmTransaction);

            shopper.activeShopChange += ShopChanged;

            ShopChanged();
        }

        private void ShopChanged()
        {
            if(currentShop != null)
            {
                currentShop.onChange -= RefreshUI;
            }

            currentShop = shopper.GetActiveShop();

            currentShop.onChange += RefreshUI;

            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (CSS_ShopItem item in currentShop.GetFilteredItems())
            {
                CSS_RowUI row = Instantiate<CSS_RowUI>(rowPrefab, listRoot);
                row.Setup(currentShop, item);
            }

            totalField.text = $"Total: ${currentShop.TransactionTotal()}";
            totalField.color = currentShop.HasSufficientFunds() ? originalTotalTextColor : Color.red;
            confirmButton.interactable = currentShop.CanTransact();
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }
    }
}