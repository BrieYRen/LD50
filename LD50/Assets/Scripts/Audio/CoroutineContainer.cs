using System.Collections;
using UnityEngine;

/// <summary>
/// this script will be attach to each music source so as to run coroutines by AudioManager
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class CoroutineContainer : MonoBehaviour
{
    AudioSource audioSource;
    AudioManager audioManager;

    [HideInInspector]
    public string audioSourceKey;


    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// public coroutine to replay the music at given time
    /// </summary>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    public IEnumerator ReplayMusicInSec(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        audioSource.enabled = false;
        audioSource.enabled = true;
    }

    /// <summary>
    /// public coroutine to change the currentMelody variable in audio manager
    /// </summary>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    public IEnumerator ChangeCurrentMelodyString(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        audioManager.currentMelody = audioSourceKey;
    }

    /// <summary>
    /// public coroutine to change the currentAccompany variable in audio manager
    /// </summary>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    public IEnumerator ChangeCurrentAccompanyString(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        audioManager.currentAccompany = audioSourceKey;
    }

}
