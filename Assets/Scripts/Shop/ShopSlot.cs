using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text amountInStock;
    [SerializeField] public TMP_Text purchaseValue;
    public Image icon;

    ShopItem shopItem;

    public void AddItem(ShopItem newItem)
    {
        shopItem = newItem;

        itemName.text = shopItem.GetItem().ItemName;
        amountInStock.text = shopItem.GetAmountInStock().ToString();
        purchaseValue.text = shopItem.GetPurchaseValue().ToString();

        icon.sprite = shopItem.GetItem().Icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        shopItem = null;

        itemName.text = "";
        amountInStock.text = "";
        purchaseValue.text = "";

        icon.sprite = null;
        icon.enabled = false;
    }

}
