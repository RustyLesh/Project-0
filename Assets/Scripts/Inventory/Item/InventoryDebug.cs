using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project0.Inventories;

public class InventoryDebug : MonoBehaviour
{
    [SerializeField] InventoryItem firstItem;
    [SerializeField] InventoryItem secondItem;

    GameObject playerShip;
    Inventory playerInventory;

    private void Start()
    {
        playerShip = GameObject.FindWithTag("PlayerShip");
        playerInventory = playerShip.GetComponent<Inventory>();
    }

    public void addFirstItem()
    {
        playerInventory.AddToFirstEmptySlot(firstItem, 1);
    }

    public void addSecondItem()
    {
        playerInventory.AddToFirstEmptySlot(secondItem, 1);
    }


}
