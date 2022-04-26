using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1000)]
public class Item : ScriptableObject
{
    [SerializeField]string itemName;
    [SerializeField]Sprite icon;

    public string ItemName
    {
        private set => itemName = value;
        get => itemName;
    }

    public Sprite Icon
    {
        private set => icon = value;
        get => icon;
    }
}
