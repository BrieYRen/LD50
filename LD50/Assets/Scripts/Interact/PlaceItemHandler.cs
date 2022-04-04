using System.Collections.Generic;
using UnityEngine;

public class PlaceItemHandler : MonoBehaviour
{
    public Dictionary<Item, PlaceItem> itemPlaceKVPs = new Dictionary<Item, PlaceItem>();


    private void Start()
    {
        InitKVP();

    }

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
