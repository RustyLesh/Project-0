using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]string itemName;
    [SerializeField]Texture2D sprite;

    public string getItemName()
    {
        return itemName;
    }

    public Texture2D getSprite()
    {
        return sprite;
    }
}
