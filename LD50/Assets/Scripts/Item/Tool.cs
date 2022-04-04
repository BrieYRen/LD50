using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Inventory/Tool")]
public class Tool : Item
{
    Craft formula;

    [Header("Tool Settings")]

    public Texture2D toolCursor;
    public Vector2 toolCursorOffset;

    public override void Use()
    {
        formula = GameManager.instance.craftManager.formulas[this];

        if (!formula.CanCraft)
            formula.ActivateTool();
        else
            formula.DeactiveTool();
    }

}