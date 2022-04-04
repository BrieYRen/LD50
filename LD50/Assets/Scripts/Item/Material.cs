using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material Item")]
public class Material : Item
{
    Craft formula;

    public override void Use()
    {
        formula = GameManager.instance.craftManager.formulas[this];

        if (formula.CanCraft)
            formula.CraftProduct(this);          
        else 
            base.Use();
    }

}
