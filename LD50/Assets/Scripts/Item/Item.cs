using UnityEngine;

/// <summary>
/// the base class for all kinds of items
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    [TextArea] public string itemInfo;

    public virtual void Use()
    {
        // to be override
    }
}
