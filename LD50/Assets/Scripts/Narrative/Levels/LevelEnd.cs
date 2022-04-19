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
    const int endFrame = 18; // todo
    const float endAnimTime = 20f; //todo

    [SerializeField]
    UIPanel endMenu;

    AudioManager audioManager;

    const string endingMelodyName = ""; //todo
    const string endingAccompanyName = ""; //todo
    const int delayBars = 0;

    const string endingSoundName = ""; //todo


    private void Start()
    {
        audioManager = GameManager.instance.audioManager;

        PlayEndingCG();
        StartCoroutine(ShowMainMenu(endAnimTime + 2f));
    }

    void PlayEndingCG()
    {
        audioManager.PlayIfHasTwoLayerMusic(endingMelodyName, endingAccompanyName, true, delayBars);
        audioManager.PlayIfHasAudio(endingSoundName, .1f);
        //endAnim.PlayOnce(startFrame, endFrame, animRate);
    }

    IEnumerator ShowMainMenu(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        endMenu.Show();
    }

}
