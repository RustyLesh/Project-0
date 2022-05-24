/*
 * Author: Peter An
 * 
 * Manages the front-end of the shop
 * The shop logics is handled in CSS_Shop and this makes
 * the shop intractable.
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CSS_ShopUI : MonoBehaviour
{
    [SerializeField] CSS_MoneyManager moneyManager;
    [SerializeField] TMP_Text currentCoinsText;

    Shop shop;
    public ShopSlot[] slots;
    public GameObject[] shopSlotsGO;
    public Button[] purchaseButtons;
    

    void Start()
    {
        // Debug use
        moneyManager = FindObjectOfType<CSS_MoneyManager>();
        

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
        currentCoinsText.text = "Coins: " + moneyManager.money;

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
            if (moneyManager.money >= shop.GetItemAtIndex(i).GetPurchaseValue() &&
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
        // if we do have enough coins
        if (moneyManager.money >= shop.GetItemAtIndex(btnNo).GetPurchaseValue())
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
        currentCoinsText.text = "Coins: " + moneyManager.money;
    }
}
