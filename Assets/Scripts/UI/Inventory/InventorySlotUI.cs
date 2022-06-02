using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Project0.Inventories;
using Project0.Core.UI.Dragging;

namespace Project0.UI.Inventories
{
    public class InventorySlotUI : MonoBehaviour, IItemHolder, IDragContainer<SO_InventoryItem>
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;

        // STATE
        int index;
        SO_InventoryItem item;
        CSS_Inventory inventory;

        // PUBLIC

        public void Setup(CSS_Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
            icon.SetItem(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
        }

        public int MaxAcceptable(SO_InventoryItem item)
        {
            if (inventory.HasSpaceFor(item))
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void AddItems(SO_InventoryItem item, int number)
        {
            inventory.AddItemToSlot(index, item, number);
        }

        public SO_InventoryItem GetItem()
        {
            return inventory.GetItemInSlot(index);
        }

        public int GetNumber()
        {
            return inventory.GetNumberInSlot(index);
        }

        public void RemoveItems(int number)
        {
            inventory.RemoveFromSlot(index, number);
        }
    }
}