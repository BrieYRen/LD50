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
    }

    void Blink()
    {
        vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
        blackImage.alpha = Mathf.Sin(Time.realtimeSinceStartup);
    }

    public void BeginBlink()
    {
        isBlink = true;
    }

    public void StopBlink()
    {
        isBlink = false;
    }

}
