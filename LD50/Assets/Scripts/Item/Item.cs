using UnityEngine;

/// <summary>
/// the base class for all kinds of items
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Basic Settings")]

    new public string name = "New Item";
    public Sprite icon = null;

    [Header("Place Item Settings")]

    public bool canPlace;
    public Texture2D placeCursor;
    public Vector2 placeCursorOffset;


    public virtual void Use()
    {
        ReadyToPlace();
    }

    void ReadyToPlace()
    {
        if (!canPlace)
            return;

        PlaceItem placeItem = GameManager.instance.placeItemHandler.itemPlaceKVPs[this];
        placeItem.ReadyToPlace();
    }


}
