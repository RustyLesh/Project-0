/*
 * Author: Peter An
 * 
 * This will manage the items that the player holds
 * 
 * This uses the singleton pattern so that only
 * one Inventory system exists in the scene.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one inventory instance found!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public delegate void OnItemChanged();
    public static event OnItemChanged onItemChangedCallBack;

    [SerializeField] List<Item> items = new List<Item>();

    [SerializeField] int space = 12;

    public Item debugItem;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room in inventory!");
            return false;
        }
        items.Add(item);
        onItemChangedCallBack.Invoke();
        return true;
    }

    public void AddDebugItem()
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room in inventory!");
            return;
        }
        items.Add(debugItem);
        onItemChangedCallBack.Invoke();
    }

    public bool Remove(Item item)
    {
        if(items.Count <=0 )
        {
            Debug.Log("Inventory is empty to remove!");
            return false;
        }
        items.Remove(item);
        onItemChangedCallBack.Invoke();
        return true;
    }

    public void RemoveDebugItem()
    {
        if (items.Count <= 0)
        {
            Debug.Log("Inventory is empty to remove!");
            return;
        }
        items.RemoveAt(items.Count-1);
        onItemChangedCallBack.Invoke();
    }

    public int GetItemCount() { return items.Count; }

    public Item GetItemIndex(int index) { return items[index]; }
}
