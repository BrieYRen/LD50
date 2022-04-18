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

    [Tooltip("the sound effect when activate the tool and the cursor changes to tool")]
    public string toolCursorSfx;

    [Tooltip("the sound effect when de-active the tool and the cursor changes to normal")]
    public string resumeCursorSfx;

    [Tooltip("the sound effect when use the tool and sucessfully crafts a product")]
    public string useToolSfx;


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