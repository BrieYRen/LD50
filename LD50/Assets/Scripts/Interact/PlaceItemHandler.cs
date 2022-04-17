using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is to manage all placable items and their placing functions including init and placing status check
/// </summary>
public class PlaceItemHandler : MonoBehaviour
{
    public Dictionary<Item, PlaceItem> itemPlaceKVPs = new Dictionary<Item, PlaceItem>();


    private void Start()
    {
        InitKVP();
    }

    /// <summary>
    /// init all placable items and their place functions at the beginning of the game for furthur use
    /// </summary>
    void InitKVP()
    {
        PlaceItem[] placeItems = GetComponentsInChildren<PlaceItem>();

        if (placeItems.Length == 0)
            return;

        for (int i = 0; i < placeItems.Length; i++)
        {
            itemPlaceKVPs.Add(placeItems[i].item, placeItems[i]);
        }
    }

    /// <summary>
    /// public method to detect if any placable item is activated
    /// </summary>
    /// <returns></returns>
    public bool CheckIfPlacing()
    {
        PlaceItem[] placeItems = GetComponentsInChildren<PlaceItem>();

        if (placeItems.Length == 0)
            return false;

        for (int i = 0; i < placeItems.Length; i++)
        {
            if (placeItems[i].canPlace)
                return true;
        }

        return false;
    }

}
