using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAddItem : MonoBehaviour
{
    Shop shop;

    [SerializeField] Item item;
    [SerializeField] int amount = 1;
    [SerializeField] int price = 1;

    void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    public void AddItemToShop()
    {
        Debug.Log("Adding: " + item.ItemName + " to the shop");
        shop.AddItemIntoShop(item, amount, price);
        Debug.Log(shop.GetItemCount());
    }

    public void DeleteLastItemInShop()
    {
        shop.DeleteItemInInventory(shop.GetItemCount() - 1);
    }
}
