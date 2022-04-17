using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is the inventory manager script
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// the public delegate to invoke ui update methods when the items changed in inventory
    /// </summary>
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    [Tooltip("how many items in max are allowed to put in inventory")]
    public int space = 6;

    [HideInInspector]
    public List<Item> items = new List<Item>();


    /// <summary>
    /// public method to add an item to the inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Add(Item item)
    {
        if (items.Count >= space)
            return false;

        items.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    /// <summary>
    /// public method to remove an item from the inventory
    /// </summary>
    /// <param name="item"></param>
    public void Removed(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
