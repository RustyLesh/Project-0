/*
 * Author: Peter An
 * 
 * This will the components in the inventory system
 * so that the inventory system can be displayed onto the canvas
 * 
 */

using Project0;
using UnityEngine;



public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {

    }

    void UpdateUI()
    {
        //Debug.Log("Updating UI");
        // loop through all inventory slots
        for(int i = 0; i < slots.Length; i++)
        {
            //if (i < inventory.items.Count)
            //{
            //    // slots[i].AddItem(inventory.items[i]);
            //}
            //else
            //{
            //    // slots[i].AddItem(inventory.items[i]);
            //}
        }
    }
}


