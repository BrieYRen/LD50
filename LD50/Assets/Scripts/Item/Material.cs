using UnityEngine;

/// <summary>
/// this is a derived class from the base Item class for material items
/// </summary>
[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material Item")]
public class Material : Item
{
    Craft formula;

    /// <summary>
    /// triggered when player left click the item in inventory
    /// </summary>
    public override void Use()
    {
        formula = GameManager.instance.craftManager.formulas[this];

        if (formula.CanCraft)
            formula.CraftProduct(this);          
        else 
            base.Use();
    }

}
