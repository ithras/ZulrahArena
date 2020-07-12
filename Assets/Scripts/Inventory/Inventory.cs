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
            Debug.LogWarning("More tha one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public Item[] items = new Item[28];
    public int itemCount = 0;


    int findEmptySlot()
    {
        int slot = 0;

        while(items[slot] != null)
        {
            slot++;
        }

        return slot++;
    }

    public void Consumable(Item item)
    {
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void Add(Item item)
    {
        if(itemCount >= items.Length)
        {
            Debug.Log("Not enough room.");
            return;
        }
        item.assignedSlot = findEmptySlot();
        items[item.assignedSlot] = item;

        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        itemCount++;
    }

    public void Swap(Item oldItem)
    {

        items[oldItem.assignedSlot] = oldItem;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items[item.assignedSlot] = null;
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        itemCount--;
    }
}
