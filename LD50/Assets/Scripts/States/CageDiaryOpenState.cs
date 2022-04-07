using UnityEngine;
using UnityEngine.UI;

public class CageDiaryOpenState : State
{
    [Header("Specific Settings")]

    [SerializeField]
    Image image;

    [SerializeField]
    Button pickupOpenDiaryButton;

    CanvasGroup canvasGroup;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        image.enabled = true;
        pickupOpenDiaryButton.interactable = true;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        image.enabled = false;
        pickupOpenDiaryButton.interactable = false;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

}
