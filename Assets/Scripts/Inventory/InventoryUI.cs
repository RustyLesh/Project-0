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

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    void OnEnable()
    {
        Inventory.onItemChangedCallBack += UpdateUI;

    }

    void OnDisable()
    {
        Inventory.onItemChangedCallBack -= UpdateUI;
    }

    void UpdateUI()
    {
        //Debug.Log("Updating UI");
        
        // loop through all inventory slots
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.GetItemCount())
            {
                slots[i].AddItem(inventory.GetItemIndex(i));
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}


