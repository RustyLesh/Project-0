using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project0.Shops
{
    public class CSS_Shopper : MonoBehaviour
    {
        [SerializeField] CSS_Shop activeShop = null;

        public event Action activeShopChange;

        public void SetActiveShop(CSS_Shop shop)
        {

            activeShop = shop;
            activeShop.SetShopper(this);

            if (activeShopChange != null)
            {
                activeShopChange.Invoke();
            }
        }

        public CSS_Shop GetActiveShop()
        {
            return activeShop;
        }

        public CSS_MoneyManager GetPurse()
        {
            return CSS_MoneyManager.Instance;
        }
    }

}

