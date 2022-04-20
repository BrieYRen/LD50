using System.Collections;
using UnityEngine;

/// <summary>
/// throwaway code for ending scene's specific narrative requirements
/// </summary>
public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    AnimFrames endAnim;

    const float animRate = .5f;
    const int startFrame = 0;
    const int endFrame = 18; // todo
    const float endAnimTime = 40f; //todo

    [SerializeField]
    UIPanel endMenu;

    AudioManager audioManager;

    const string endingMelodyName = ""; //todo
    const string endingAccompanyName = ""; //todo
    const int delayBars = 0;

    const string endingSoundName = ""; //todo

    [SerializeField]
    BlinkEffect blinkEffect;

    const float stopBlinkTime = 19f; //todo


    private void Start()
    {
        audioManager = GameManager.instance.audioManager;

        PlayEndingCG();

        StartCoroutine(ShowMainMenu(endAnimTime + 2f));
        StartCoroutine(StopBlink(stopBlinkTime));
    }


    void PlayEndingCG()
    {
        audioManager.PlayIfHasTwoLayerMusic(endingMelodyName, endingAccompanyName, true, delayBars);
        audioManager.PlayIfHasAudio(endingSoundName, .1f);
        //endAnim.PlayOnce(startFrame, endFrame, animRate);
    }

    IEnumerator StopBlink(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        blinkEffect.StopBlink();
    }

    IEnumerator ShowMainMenu(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        endMenu.Show();
    }

}
