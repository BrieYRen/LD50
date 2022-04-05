using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    public CanvasGroup freeplaceParent;
    public string areaTag;


    private void Start()
    {
        cursorSwitcher = GameManager.instance.cursorSwitcher;
        inventory = GameManager.instance.inventoryManager;

        freeplaceParent.blocksRaycasts = false;
        freeplaceParent.alpha = 0f;

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

        freeplaceParent.blocksRaycasts = true;
        freeplaceParent.alpha = 1f;

        cursorSwitcher.SetToCursor(item.placeCursor, item.placeCursorOffset);
    }

    public void BackNormal()
    {        
        canPlace = false;

        freeplaceParent.blocksRaycasts = false;
        freeplaceParent.alpha = 0f;

        cursorSwitcher.SetNormal();
    }

    public void OnClickPlaceButton()
    {
        // check if the click position is within the free place area
        bool canFreePlace = false;

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            for (int i = 0; i < raycastResults.Count; i++)
            {
                if (raycastResults[i].gameObject.CompareTag(areaTag))
                    canFreePlace = true;
            }
        }

        // if it's in free place area   
        if (canFreePlace)
        {
            // place the interactable object at mouse position 
            Vector2 mousePos = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(toPlaceCanvas.transform as RectTransform, Input.mousePosition, toPlaceCanvas.worldCamera, out mousePos);

            itemToSpawn.transform.position = toPlaceCanvas.transform.TransformPoint(mousePos);
            itemToSpawn.gameObject.SetActive(true);

            // remove from inventory 
            inventory.Removed(item);
        }
        

        // leave the ready to place state
        BackNormal();
    }


    // destroy the spawned items on level change
    void OnSceneUnloaded(Scene scene)
    {
        itemToSpawn.gameObject.SetActive(false);
    }
}
