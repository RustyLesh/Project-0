using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]string itemName;
    [SerializeField]Texture2D sprite;

    public string GetItemName()
    {
        return itemName;
    }

    public Texture2D GetSprite()
    {
        return sprite;
    }
}
