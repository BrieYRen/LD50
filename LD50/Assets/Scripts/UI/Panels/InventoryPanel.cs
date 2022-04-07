using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryPanel : UIPanel
{
    [Header("Inventory")]
    [Tooltip("the parent game object of all inventory slots")]
    public Transform itemsParent;

    Inventory inventory;
    Slot[] inventorySlots;


    protected override void OnStart()
    {
        base.OnStart();

        inventory = GameManager.instance.inventoryManager;
        inventory.onItemChangedCallback += UpdateUI;

        inventorySlots = itemsParent.GetComponentsInChildren<Slot>();

        SceneManager.sceneUnloaded += OnSceneUnloaded;

        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
                inventorySlots[i].AddItem(inventory.items[i]);
            else
                inventorySlots[i].ClearSlot();
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        this.Close();
    }
}
