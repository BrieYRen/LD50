using UnityEngine;

/// <summary>
/// the base class for all kinds of items
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Basic Settings")]

    [Tooltip("name of the item")]
    new public string name = "New Item";

    [Tooltip("icon of the item to be shown in inventory")]
    public Sprite icon = null;

    [Header("Place Item Settings")]

    [Tooltip("check true if the item can be placed from inventory to the environment")]
    public bool canPlace;

    [Tooltip("the cursor image to switch when the item is placing")]
    public Texture2D placeCursor;

    [Tooltip("the offset of the placing mouse cursor")]
    public Vector2 placeCursorOffset;

    /// <summary>
    /// triggered when player left click the item in inventory
    /// </summary>
    public virtual void Use()
    {
        ReadyToPlace();
    }

    /// <summary>
    /// if the item is placable, then activate its placing procedure
    /// </summary>
    void ReadyToPlace()
    {
        if (!canPlace)
            return;

        PlaceItem placeItem = GameManager.instance.placeItemHandler.itemPlaceKVPs[this];
        placeItem.ReadyToPlace();
    }


}
