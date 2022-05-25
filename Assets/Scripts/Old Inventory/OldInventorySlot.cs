using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OldInventorySlot : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;

    public Image icon;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        itemName.text = item.name;

        icon.sprite = item.Icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        itemName.text = "Empty";

        icon.sprite = null;
        icon.enabled = false;
    }
}

