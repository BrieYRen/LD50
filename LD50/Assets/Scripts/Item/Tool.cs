using UnityEngine;

/// <summary>
/// this is a derived class from the base Item class for tool items
/// </summary>
[CreateAssetMenu(fileName = "New Tool", menuName = "Inventory/Tool")]
public class Tool : Item
{
    Craft formula;

    [Header("Tool Settings")]

    [Tooltip("the cursor image to switch when the tool is activated")]
    public Texture2D toolCursor;

    [Tooltip("the offset of the tool mouse cursor")]
    public Vector2 toolCursorOffset;

    [Tooltip("check false if the tool can craft both material items in inventory and FSM in environment")]
    public bool inventoryOnly = true;


    /// <summary>
    /// triggerd when player left click the tool item in inventory
    /// </summary>
    public override void Use()
    {
        formula = GameManager.instance.craftManager.formulas[this];

        if (!formula.CanCraft)
            formula.ActivateTool();
        else
            formula.DeactiveTool();
    }

}