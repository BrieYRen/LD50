using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    Item item;

    [SerializeField]
    GameObject destroyAfterPickup;

    Inventory inventory;

    private void Start()
    {
        inventory = GameManager.instance.inventoryManager;
    }

    public void PickUp()
    {
        bool wasPickup = inventory.Add(item);

        if (wasPickup && destroyAfterPickup != null)
            Destroy(destroyAfterPickup);
    }
}
