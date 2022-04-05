using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public Dictionary<Item, Craft> formulas = new Dictionary<Item, Craft>();

    private void Start()
    {
        InitFormulas();
    }

    void InitFormulas()
    {
        Craft[] crafts = GetComponentsInChildren<Craft>();

        if (crafts.Length == 0)
            return;

        for (int i = 0; i < crafts.Length; i++)
        {
            formulas.Add(crafts[i].toolItem, crafts[i]);
            for (int j = 0; j < crafts[i].materials.Count; j++)
            {
                formulas.Add(crafts[i].materials[j], crafts[i]);
            }           
        }
    }

    public bool CheckIfCrafting()
    {
        Craft[] crafts = GetComponentsInChildren<Craft>();

        if (crafts.Length == 0)
            return false;

        for (int i = 0; i < crafts.Length; i++)
        {
            if (crafts[i].CanCraft)
                return true;
        }

        return false;
    }

}
