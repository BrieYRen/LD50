using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// derived class from the base UIPanel class for inventory ui panel
/// </summary>
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

    /// <summary>
    /// update the inventory ui panel and its slots when the items list in inventory manager has changed
    /// </summary>
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

    /// <summary>
    /// close the ui panel when a new scene is loaded
    /// </summary>
    /// <param name="scene"></param>
    void OnSceneUnloaded(Scene scene)
    {
        this.Close();
    }
}
