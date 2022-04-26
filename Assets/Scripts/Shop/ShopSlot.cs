using UnityEngine.UI;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public Image icon;

    public ShopItem shopItem;

    public void AddItem(ShopItem newItem)
    {
        shopItem = newItem;

        icon.sprite = shopItem.GetItem().Icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        shopItem = null;

        icon.sprite = null;
        icon.enabled = false;
    }

}
