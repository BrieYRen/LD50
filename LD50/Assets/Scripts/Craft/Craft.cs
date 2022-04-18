using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script can store a craft formula with a tool item and it's materials (material items or a FSM state) and products (product items or another FSM state)
/// </summary>
public class Craft : MonoBehaviour
{
    [Tooltip("darg and drop the tool item here")]
    public Tool toolItem;

    [Tooltip("material items the tool can craft")]
    public List<Item> materials;

    [Tooltip("product items in the same order with materials")]
    [SerializeField]
    List<Item> products;

    [Tooltip("FSM the tool can craft")]
    [SerializeField]
    StateMachine stateMachine;

    [Tooltip("the material state the tool can craft")]
    [SerializeField]
    State materialState;

    [Tooltip("the product state of the FSM")]
    [SerializeField]
    State productState;

    /// <summary>
    /// readonly bool to detect if the tool is activated
    /// </summary>
    protected bool canCraft = false;
    public bool CanCraft
    {
        get
        {
            return canCraft;
        }
    }

    Inventory inventory;
    CursorSwitcher cursorSwitcher;
    AudioManager audioManager;
    PlaceItemHandler handler;
    CraftManager craftManager;


    private void Start()
    {
        inventory = GameManager.instance.inventoryManager;
        cursorSwitcher = GameManager.instance.cursorSwitcher;
        audioManager = GameManager.instance.audioManager;
        handler = GameManager.instance.placeItemHandler;
        craftManager = GameManager.instance.craftManager;
    }

    private void Update()
    {
        if (canCraft && Input.GetMouseButtonDown(1))
            DeactiveTool();
    }

    /// <summary>
    /// public method to activate the tool 
    /// triggered when player left click the tool item in inventory
    /// </summary>
    public void ActivateTool()
    {
        // check if it's in placing or in other crafting procedure
        if (handler.CheckIfPlacing() || craftManager.CheckIfCrafting())
            return;

        // change mouse cursor with sfx
        cursorSwitcher.SetToCursor(toolItem.toolCursor, toolItem.toolCursorOffset);
        audioManager.PlayIfHasAudio(toolItem.toolCursorSfx, 0f);

        // ready to craft
        canCraft = true;

    }

    /// <summary>
    /// public method to de-active the tool
    /// triggered when player right click anywhere or finish crafting a product
    /// </summary>
    public void DeactiveTool()
    {
        // resume mouse cursor with sfx
        cursorSwitcher.SetNormal();
        audioManager.PlayIfHasAudio(toolItem.resumeCursorSfx, 0f);

        // dis-allow craft
        canCraft = false;

    }

    /// <summary>
    /// public method to craft a product item
    /// triggered by left click the material item in inventory
    /// </summary>
    /// <param name="material"></param>
    public void CraftProduct(Item material)
    {
        if (!canCraft)
            return;

        if (materials.Contains(material))
        {
            inventory.Removed(material);

            int index = materials.IndexOf(material);           
            inventory.Add(products[index]);

            audioManager.PlayIfHasAudio(toolItem.useToolSfx, 0f);
            audioManager.PlayIfHasAudio(products[index].pickupSfx, 0f);
        }

        DeactiveTool();
    }

    /// <summary>
    /// public method to craft a product FSM state
    /// triggered by left click the button of state machine
    /// </summary>
    public void ChangeStateMachine()
    {
        if (!canCraft)
            return;

        if (stateMachine.CurrentState == materialState)
        {
            stateMachine.ChangeCurrentState(productState);
            audioManager.PlayIfHasAudio(toolItem.useToolSfx, 0f);
        }

        DeactiveTool();
    }

}
