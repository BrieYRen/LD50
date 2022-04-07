using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public Tool toolItem;

    public List<Item> materials;

    [SerializeField]
    List<Item> products;

    [SerializeField]
    StateMachine stateMachine;

    [SerializeField]
    State materialState;

    [SerializeField]
    State productState;

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
    CraftManager craftManager;


    private void Start()
    {
        inventory = GameManager.instance.inventoryManager;
        cursorSwitcher = GameManager.instance.cursorSwitcher;
        handler = GameManager.instance.placeItemHandler;
        craftManager = GameManager.instance.craftManager;
    }

    private void Update()
    {
        if (canCraft && Input.GetMouseButtonDown(1))
            DeactiveTool();
    }

    public void ActivateTool()
    {
        // check if it's in placing
        if (handler.CheckIfPlacing() || craftManager.CheckIfCrafting())
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

    public void ChangeStateMachine()
    {
        if (!canCraft)
            return;

        if (stateMachine.CurrentState == materialState)
        {
            stateMachine.ChangeCurrentState(productState);
        }

        DeactiveTool();
    }

}
