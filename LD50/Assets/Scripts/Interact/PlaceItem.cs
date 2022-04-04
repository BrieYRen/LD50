using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// the base class to place an item from inventory to environment
/// </summary>
public class PlaceItem : MonoBehaviour
{
    public Item item;

    public Canvas toPlaceCanvas;
   
    public bool canPlace = false;

    CursorSwitcher cursorSwitcher;
    Inventory inventory;

    public RectTransform itemToSpawn;


    private void Start()
    {
        cursorSwitcher = GameManager.instance.cursorSwitcher;
        inventory = GameManager.instance.inventoryManager;

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void Update()
    {
        if (canPlace && Input.GetMouseButtonDown(0))
            OnClickPlaceButton();
    }

    public void ReadyToPlace()
    {
        canPlace = true;

        cursorSwitcher.SetToCursor(item.placeCursor, item.placeCursorOffset);
    }

    public void BackNormal()
    {        
        canPlace = false;

        cursorSwitcher.SetNormal();
    }

    public void OnClickPlaceButton()
    {
        // spawn an interactable object at mouse position      
        Vector2 mousePos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(toPlaceCanvas.transform as RectTransform, Input.mousePosition, toPlaceCanvas.worldCamera, out mousePos);

        itemToSpawn.transform.position = toPlaceCanvas.transform.TransformPoint(mousePos);
        itemToSpawn.gameObject.SetActive(true);

        // remove from inventory 
        inventory.Removed(item);

        // leave the ready to place state
        BackNormal();
    }

    // destroy the spawned items on level change
    void OnSceneUnloaded(Scene scene)
    {
        itemToSpawn.gameObject.SetActive(false);
    }
}
