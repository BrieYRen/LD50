using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageNormalState : State
{
    [Header("Specific Settings")]

    [SerializeField]
    Image image;

    [SerializeField]
    Button craftButton;

    CanvasGroup canvasGroup;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        image.enabled = true;
        craftButton.interactable = true;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        image.enabled = false;
        craftButton.interactable = false;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
}
