using System.Collections;
using UnityEngine;

/// <summary>
/// throwaway code for ending scene's specific narrative requirements
/// </summary>
public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    AnimFrames endAnim;

    const float animRate = 1f;
    const int startFrame = 0;
    const int endFrame = 49; 
    const float endAnimTime = 47f;

    [SerializeField]
    UIPanel endMenu;

    [SerializeField]
    UIPanel exPanel;

    [SerializeField]
    UIPanel parentPanel;

    [SerializeField]
    UIPanel catPanel;

    AudioManager audioManager;

    const string endingMelodyName = "BGMEndingMelody";
    const string endingAccompanyName = "BGMEndingAccompany";
    const int delayBars = 0;

    [SerializeField]
    BlinkEffect blinkEffect;

    const float blinkTime = 24f; 

    const float exTime = 33f;
    const float parentTime = 36.5f;
    const float catTime = 41.5f;


    private void Start()
    {
        audioManager = GameManager.instance.audioManager;

        PlayEndingCG();

        StartCoroutine(ShowMainMenu(endAnimTime + 2f));
        StartCoroutine(BlinkOnce(blinkTime));

        StartCoroutine(ShowText(exTime, exPanel, 2f));
        StartCoroutine(ShowText(parentTime, parentPanel, 2f));
        StartCoroutine(ShowText(catTime, catPanel, 3.5f));
    }


    void PlayEndingCG()
    {
        audioManager.PlayIfHasTwoLayerMusic(endingMelodyName, endingAccompanyName, true, delayBars);
        endAnim.PlayOnce(startFrame, endFrame, animRate);
    }


    IEnumerator BlinkOnce(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        blinkEffect.BeginBlink();

        yield return new WaitForSecondsRealtime(2.5f);

        blinkEffect.StopBlink();
    }

    IEnumerator ShowMainMenu(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        endMenu.Show();
    }

    IEnumerator ShowText(float waitTime, UIPanel panel, float remainTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        panel.Show();

        yield return new WaitForSecondsRealtime(remainTime);
        panel.Close();
    }

}
