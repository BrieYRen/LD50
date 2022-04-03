using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInteractChoices : MonoBehaviour
{
    [SerializeField]
    Button[] buttons;

    bool isInteracting = false;

    public void OnClickInteract()
    {
        isInteracting = !isInteracting;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = isInteracting;
        }
    }
}
