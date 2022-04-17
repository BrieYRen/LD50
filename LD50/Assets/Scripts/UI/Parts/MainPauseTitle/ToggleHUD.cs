using UnityEngine;

/// <summary>
/// the script to hide or show HUD before or after cutscenes
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class ToggleHUD : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// public method to show the hidden HUD
    /// </summary>
    public void ShowHUD()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// public method to hide the HUD
    /// </summary>
    public void HideHUD()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
