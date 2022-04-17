using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this script is to toggle the interactable status of the interactable objects in levels
/// </summary>
public class ShowInteractChoices : MonoBehaviour
{
    [Tooltip("drag and drop the interactable choices buttons here")]
    [SerializeField]
    Button[] buttons;

    bool isInteracting = false;

    CraftManager craftManager;
    PlaceItemHandler handler;


    private void Start()
    {
        craftManager = GameManager.instance.craftManager;
        handler = GameManager.instance.placeItemHandler;
    }

    /// <summary>
    /// public method to toggle the interactable status
    /// </summary>
    public void OnClickInteract()
    {
        if (craftManager.CheckIfCrafting() || handler.CheckIfPlacing())
            return;

        isInteracting = !isInteracting;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = isInteracting;
        }
    }

}
