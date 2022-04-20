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

    float startBlinkTime;

    bool isBlink = false;
    bool toStop = false;


    private void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out Vignette newVignette);
        vignette = newVignette;
    }

    private void Update()
    {       
        Blink();

        if (toStop)
            ReadyToStop();

    }

    void Blink()
    {
        if (!isBlink || startBlinkTime == 0)
            return;
        
        vignette.intensity.value = Mathf.Sin(Time.time - startBlinkTime);
        blackImage.alpha = Mathf.Sin(Time.time - startBlinkTime);
          
    }

    void ReadyToStop()
    {
        if (blackImage.alpha == 0)
        {
            toStop = false;
            isBlink = false;
        }
    }

    /// <summary>
    /// public method to activate the blink effect
    /// </summary>
    public void BeginBlink()
    {
        startBlinkTime = Time.time;
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
