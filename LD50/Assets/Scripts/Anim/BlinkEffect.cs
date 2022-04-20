using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// this script is to simulate a blink effect
/// </summary>
[RequireComponent(typeof(PostProcessVolume))]
public class BlinkEffect : MonoBehaviour
{
    [SerializeField]
    CanvasGroup blackImage;

    PostProcessVolume volume;
    Vignette vignette;

    bool isBlink = true;
    bool toStop = false;


    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out Vignette newVignette);
        vignette = newVignette;
    }

    private void Update()
    {
        if (isBlink)
            Blink();

        if (toStop)
        {
            if (blackImage.alpha == 0)
                isBlink = false;
        }

    }

    void Blink()
    {
        vignette.intensity.value = Mathf.Sin(Time.time);
        blackImage.alpha = Mathf.Sin(Time.time);
    }

    /// <summary>
    /// public method to activate the blink effect
    /// </summary>
    public void BeginBlink()
    {
        isBlink = true;
    }

    /// <summary>
    /// public method to stop blinking at next eye open wide moment
    /// </summary>
    public void StopBlink()
    {
        toStop = true;
    }


}
