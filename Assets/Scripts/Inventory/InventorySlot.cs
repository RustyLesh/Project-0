using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Sprite icon;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon = item.Icon;
    }

    public void ClearSlot()
    {
        item = null;

        icon = null;
    }
}

