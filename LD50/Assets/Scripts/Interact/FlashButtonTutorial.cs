using UnityEngine;

/// <summary>
/// this script is a tutorial to flash the play button when player placed an item for the first time
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class FlashButtonTutorial : MonoBehaviour
{
    [SerializeField]
    RectTransform toPlaceObjectsParent;

    RectTransform[] toPlaceObjects;

    [SerializeField]
    Animator animator;

    CanvasGroup canvasGroup;

    PlaceItemHandler placeItemsParents;
    PlaceItem[] placeItems;

    int triggeredTime = 0;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        placeItemsParents = GameManager.instance.placeItemHandler;
        placeItems = placeItemsParents.GetComponentsInChildren<PlaceItem>();
        for (int i = 0; i < placeItems.Length; i++)
            placeItems[i].placedItemCallback += FlashCheck;
    }

    void FlashCheck()
    {
        toPlaceObjects = toPlaceObjectsParent.GetComponentsInChildren<RectTransform>();
        if (toPlaceObjects.Length != 0)
            BeginFlash();
    }

    void BeginFlash()
    {
        triggeredTime++;

        if (triggeredTime != 1)
            return;

        animator.enabled = true;
    }

    public void OnClickContinue()
    {
        if (triggeredTime < 1)
            return;

        animator.enabled = false;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
