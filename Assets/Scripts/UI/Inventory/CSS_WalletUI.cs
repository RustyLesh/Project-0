using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Project0.Shops;

namespace Project0.UI
{
    public class CSS_WalletUI : MonoBehaviour
    {
        [SerializeField] TMP_Text balanceField;

        CSS_MoneyManager playerWallet = null;

        void Start()
        {
            playerWallet = CSS_MoneyManager.Instance;

            if (playerWallet != null)
            {
                playerWallet.onChange += RefreshUI;
            }
            
            RefreshUI();
        }

        void RefreshUI()
        {
            balanceField.text = $"${playerWallet.money}";
        }
    }
}