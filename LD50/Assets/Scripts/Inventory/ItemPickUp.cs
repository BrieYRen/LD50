using UnityEngine;

/// <summary>
/// this is a script to pick up an item from environment to inventory
/// </summary>
public class ItemPickUp : MonoBehaviour
{
    [Tooltip("the item to pick up")]
    [SerializeField]
    Item item;

    [Tooltip("the object to destroy after picked up the item")]
    [SerializeField]
    GameObject destroyAfterPickup;

    Inventory inventory;

    private void Start()
    {
        inventory = GameManager.instance.inventoryManager;
    }

    /// <summary>
    /// public method to pick up the item from environment to player inventory
    /// </summary>
    public void PickUp()
    {
        bool wasPickup = inventory.Add(item);

        if (wasPickup && destroyAfterPickup != null)
        {
            destroyAfterPickup.SetActive(false);
            destroyAfterPickup.GetComponent<ShowInteractChoices>().OnClickInteract();
        }
            
    }
}
