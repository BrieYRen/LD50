using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// the base class to place an item from inventory to environment
/// </summary>
public class PlaceItem : MonoBehaviour
{
    [Tooltip("the item to be placed from inventory to environment")]
    public Item item;

    [Tooltip("the ui canvas to spawn the item object")]
    public Canvas toPlaceCanvas;
    
    /// <summary>
    /// public variable to detect if it's activated
    /// </summary>
    [HideInInspector]
    public bool canPlace = false;

    CraftManager craftManager;
    PlaceItemHandler placeItemHandler;
    CursorSwitcher cursorSwitcher;
    protected AudioManager audioManager;
    protected Inventory inventory;

    [Tooltip("the ui object to spawn")]
    public RectTransform itemToSpawn;

    [Tooltip("the parent object of all free place areas")]
    public CanvasGroup freeplaceParent;

    [Tooltip("area that can free place item at mouse position")]
    public string areaTag;

    [Tooltip("plane area that can place item at a preset position")]
    public string planeTag;


    private void Start()
    {
        craftManager = GameManager.instance.craftManager;
        placeItemHandler = GameManager.instance.placeItemHandler;
        cursorSwitcher = GameManager.instance.cursorSwitcher;
        audioManager = GameManager.instance.audioManager;
        inventory = GameManager.instance.inventoryManager;

        freeplaceParent.blocksRaycasts = false;
        freeplaceParent.alpha = 0f;

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void Update()
    {
        if (canPlace && Input.GetMouseButtonDown(0))
            OnClickPlaceButton();

        if (canPlace && Input.GetMouseButtonDown(1))
            BackNormal();
    }

    /// <summary>
    /// public method to activate the placable item
    /// triggered when player left click the item in inventory
    /// </summary>
    public virtual void ReadyToPlace()
    {
        if (craftManager.CheckIfCrafting() || placeItemHandler.CheckIfPlacing())
            return;

        canPlace = true;

        freeplaceParent.blocksRaycasts = true;
        freeplaceParent.alpha = 1f;

        cursorSwitcher.SetToCursor(item.placeCursor, item.placeCursorOffset);
    }

    /// <summary>
    /// public method to de-active the placing procedure
    /// triggered when player right click anywhere or finished placing the item
    /// </summary>
    public virtual void BackNormal()
    {        
        canPlace = false;

        freeplaceParent.blocksRaycasts = false;
        freeplaceParent.alpha = 0f;

        cursorSwitcher.SetNormal();
    }

    /// <summary>
    /// public method to check if the item can be placed and where to place
    /// triggerd when the placable item is activated and player left-click the environment to place it
    /// </summary>
    public void OnClickPlaceButton()
    {
        // check if the click position is within the place area and free place area
        bool canFreePlace =  RaycastTagCheck(areaTag);
        bool canPut = RaycastTagCheck(planeTag);


        // if it's in can place area
        if (canPut)
        {
            // if it's in free place area   
            if (canFreePlace)
            {
                // place the interactable object at mouse position 
                Vector2 mousePos = new Vector2();
                RectTransformUtility.ScreenPointToLocalPointInRectangle(toPlaceCanvas.transform as RectTransform, Input.mousePosition, toPlaceCanvas.worldCamera, out mousePos);

                itemToSpawn.transform.position = toPlaceCanvas.transform.TransformPoint(mousePos);
            }

            // show placed item
            itemToSpawn.gameObject.SetActive(true);

            // remove from inventory 
            inventory.Removed(item);

            // play place sfx
            audioManager.PlayIfHasAudio(item.PlaceSfx, 0f);
        }
              

        // check other conditions when needed
        SpecificCheck();

        // leave the ready to place state
        BackNormal();
    }

    /// <summary>
    /// to be override in derived class for specific check besides free place area check
    /// </summary>
    public virtual void SpecificCheck()
    {
        // to be override
    }

    /// <summary>
    /// raycast check to see if mouse position is in certain place area
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    protected bool RaycastTagCheck(string tag)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            for (int i = 0; i < raycastResults.Count; i++)
            {
                if (raycastResults[i].gameObject.CompareTag(tag))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// destroy the spawned items on level change
    /// </summary>
    /// <param name="scene"></param>
    void OnSceneUnloaded(Scene scene)
    {
        itemToSpawn.gameObject.SetActive(false);
    }
}
