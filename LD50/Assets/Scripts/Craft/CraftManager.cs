using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is the script to manage all craft fomulas including init and active status check
/// </summary>
public class CraftManager : MonoBehaviour
{
    public Dictionary<Item, Craft> formulas = new Dictionary<Item, Craft>();

    private void Start()
    {
        InitFormulas();
    }

    /// <summary>
    /// init all craft formulas at the beginning of the game for further use
    /// </summary>
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

    /// <summary>
    /// public method to detect if any tool is activated
    /// </summary>
    /// <returns></returns>
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
