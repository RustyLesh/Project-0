using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project0.Inventories;

public class InventoryDebug : MonoBehaviour
{
    [SerializeField] InventoryItem item;

    GameObject playerShip;
    Inventory playerInventory;

    private void Start()
    {
        playerShip = GameObject.FindWithTag("PlayerShip");
        playerInventory = playerShip.GetComponent<Inventory>();
    }

    public void addItem()
    {
        playerInventory.AddToFirstEmptySlot(item, 1);
    }
}
