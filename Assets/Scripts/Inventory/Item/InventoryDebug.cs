using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project0.Inventories;

public class InventoryDebug : MonoBehaviour
{
    [SerializeField] SO_InventoryItem firstItem;
    [SerializeField] SO_InventoryItem secondItem;

    GameObject playerShip;
    CSS_Inventory playerInventory;

    private void Start()
    {
        playerShip = GameObject.FindWithTag("PlayerShip");
        playerInventory = playerShip.GetComponent<CSS_Inventory>();
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
