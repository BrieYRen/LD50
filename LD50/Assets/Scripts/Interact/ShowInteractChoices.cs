using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInteractChoices : MonoBehaviour
{
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
