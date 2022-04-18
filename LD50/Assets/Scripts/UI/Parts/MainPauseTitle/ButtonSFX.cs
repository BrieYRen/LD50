using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// derived class to add sound effect to the build-in ugui buttons
/// </summary>
public class ButtonSFX : Button
{
    const string hoverSFXName = "Hover"; 
    const string pressSFXName = "Press"; 


    public override void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();

        base.OnPointerEnter(eventData);
    }

    void PlayHoverSound()
    {
        if (hoverSFXName != null)
            GameManager.instance.audioManager.PlayIfHasAudio(hoverSFXName, 0f);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        PlayPressSound();

        base.OnPointerClick(eventData);
    }

    void PlayPressSound()
    {
        if (pressSFXName != null)
            GameManager.instance.audioManager.PlayIfHasAudio(pressSFXName, 0f);
    }
}
