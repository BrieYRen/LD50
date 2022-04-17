using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// derived class to add sound effect to the build-in ugui buttons
/// </summary>
public class ButtonSFX : Button
{
    const string hoverSFXName = "FootstepWater01"; // todo
    const string pressSFXName = "FootstepWater01"; // todo

    AudioManager audioManager;


    protected override void Start()
    {
        base.Start();

        audioManager = GameManager.instance.audioManager;
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();

        base.OnPointerEnter(eventData);
    }

    void PlayHoverSound()
    {
        if (hoverSFXName != null)
            audioManager.PlayIfHasAudio(hoverSFXName, 0f);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        PlayPressSound();

        base.OnPointerClick(eventData);
    }

    void PlayPressSound()
    {
        if (pressSFXName != null)
            audioManager.PlayIfHasAudio(pressSFXName, 0f);
    }
}
