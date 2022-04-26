using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    public Transform itemsParent;

    Shop shop;

    InventorySlot[] slots;

    void Start()
    {
        shop = FindObjectOfType<Shop>();

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

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

        // loop through all inventory slots
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shop.GetItemCount())
            {
                slots[i].AddItem(shop.GetItemAtIndex(i));
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
