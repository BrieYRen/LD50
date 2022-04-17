using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// script to be attach to each inventory ui slot 
/// </summary>
public class Slot : MonoBehaviour
{
    [HideInInspector]
    public Image icon;

    Item item;

    /// <summary>
    /// public method to add an item to this ui slot
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    /// <summary>
    /// public method to remove the current item from this ui slot
    /// </summary>
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    /// <summary>
    /// to be addlistener to the slot's button so as to use the item in this slot
    /// </summary>
    public void UseItem()
    {
        if (item != null)
            item.Use();
    }
}
