using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public Tool toolItem;

    public List<Item> materials;

    [SerializeField]
    List<Item> products;

    private bool canCraft = false;
    public bool CanCraft
    {
        get
        {
            return canCraft;
        }
    }

    Inventory inventory;
    CursorSwitcher cursorSwitcher;
    PlaceItemHandler handler;

    const float inventoryAreaX = 1177f;
    const float inventoryAreaY = 160.6f;

    private void Start()
    {
        inventory = GameManager.instance.inventoryManager;
        cursorSwitcher = GameManager.instance.cursorSwitcher;
        handler = GameManager.instance.placeItemHandler;
    }

    private void Update()
    {
        DeactiveWhenOutBagArea();
    }

    public void ActivateTool()
    {
        // check if it's in placing
        if (handler.CheckIfPlacing())
            return;

        // change mouse cursor
        cursorSwitcher.SetToCursor(toolItem.toolCursor, toolItem.toolCursorOffset);

        // ready to craft
        canCraft = true;
    }

    public void DeactiveTool()
    {
        // resume mouse cursor
        cursorSwitcher.SetNormal();

        // disallow craft
        canCraft = false;
    }

    void DeactiveWhenOutBagArea()
    {
        if (!canCraft)
            return;

        Vector3 mousePos = Input.mousePosition;
        if (mousePos.x > inventoryAreaX || mousePos.y > inventoryAreaY)
            DeactiveTool();

        
    }

    public void CraftProduct(Item material)
    {
        if (!canCraft)
            return;

        if (materials.Contains(material))
        {
            inventory.Removed(material);

            int index = materials.IndexOf(material);           
            inventory.Add(products[index]);
        }

        DeactiveTool();
    }

}
