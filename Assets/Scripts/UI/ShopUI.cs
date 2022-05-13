using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] MoneyManager moneyManager;

    Shop shop;
    public ShopSlot[] slots;
    public GameObject[] shopSlotsGO;
    public Button[] purchaseButtons;

    [SerializeField] TMP_Text currentCoinsText;

    void Start()
    {
        // Debug use
        moneyManager = FindObjectOfType<MoneyManager>();
        

        shop = FindObjectOfType<Shop>();

        UpdateUI();
    }

    void OnEnable()
    {
        Shop.OnShopChanged += UpdateUI;
    }

    void OnDisable()
    {
        Shop.OnShopChanged -= UpdateUI;
    }

    void UpdateUI()
    {
        //Debug.Log("Updating UI");
        currentCoinsText.text = "Coins: " + moneyManager.GetCoinInWallet();

        // loop through all shop slots
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shop.GetItemCount())
            {
                slots[i].AddItem(shop.GetItemAtIndex(i));
                shopSlotsGO[i].SetActive(true);
            }
            else
            {
                slots[i].ClearSlot();
                shopSlotsGO[i].SetActive(false);
            }
        }

        CheckPurchaseable();
    }

    // Debuging use
    void Update()
    {
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for(int i = 0; i < shop.GetItemCount(); i++)
        {
            // if we dont have enough coin make the button uninteractable
            if (moneyManager.PlayerWallet.CoinCount >= shop.GetItemAtIndex(i).GetPurchaseValue() &&
                                                       shop.GetItemAtIndex(i).GetAmountInStock() > 0)
            {
                purchaseButtons[i].interactable = true;
            }
            // else make it interactable
            else
            {
                purchaseButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int btnNo)
    {
        // Debug.Log("Button: " + btnNo + " has been pressed");
        // Debug.Log(shop.GetItemAtIndex(btnNo).GetItem().ItemName);
        // if we do have enough coins
        if (moneyManager.PlayerWallet.CoinCount >= shop.GetItemAtIndex(btnNo).GetPurchaseValue())
        {
            shop.PurchaseItem(btnNo);
            CheckPurchaseable();

        }
        // minus coins from wallet
        // update coin value in UI
        // CheckPurchaseable() 
    }

    // for testing use
    public void AddCoins(int coins)
    {
        moneyManager.DebugAddCoins(coins);
        currentCoinsText.text = "Coins: " + moneyManager.GetCoinInWallet();
    }
}
