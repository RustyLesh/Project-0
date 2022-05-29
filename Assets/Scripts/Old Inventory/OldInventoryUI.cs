/*
 * Author: Peter An
 * 
 * This will the components in the inventory system
 * so that the inventory system can be displayed onto the canvas
 * 
 */

using Project0;
using UnityEngine;



public class OldInventoryUI : MonoBehaviour
{
    // public Transform itemsParent;

    OldInventory inventory;

    public OldInventorySlot[] slots;

    void Start()
    {
        inventory = OldInventory.instance;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    void OnEnable()
    {
        OldInventory.onItemChangedCallBack += UpdateUI;

    }

    void OnDisable()
    {
        OldInventory.onItemChangedCallBack -= UpdateUI;
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


