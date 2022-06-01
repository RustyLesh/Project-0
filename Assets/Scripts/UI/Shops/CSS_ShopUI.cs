using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Project0.Shops;

namespace Project0.UI.Shops
{
    public class CSS_ShopUI : MonoBehaviour
    {
        CSS_Shopper shopper = null;
        [SerializeField] CSS_Shop currentShop = null;

        [SerializeField] Transform listRoot;
        [SerializeField] CSS_RowUI rowPrefab;

        void Start()
        {
            shopper = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<CSS_Shopper>();

            if (shopper == null) return;
            shopper.SetActiveShop(currentShop);

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
            //gameObject.SetActive(currentShop != null);

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
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }
    }
}