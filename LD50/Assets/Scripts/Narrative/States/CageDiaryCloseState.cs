using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is a derived class from the base class State for a state of the Cage state machine in level 1
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CageDiaryCloseState : State
{
    [Header("Specific Settings")]

    [SerializeField]
    Image image;

    [SerializeField]
    Button pickupCloseDiaryButton;

    CanvasGroup canvasGroup;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        image.enabled = true;
        pickupCloseDiaryButton.interactable = true;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        image.enabled = false;
        pickupCloseDiaryButton.interactable = false;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

}
