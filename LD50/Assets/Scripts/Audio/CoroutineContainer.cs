using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public IEnumerator ReplayMusicInSec(float delayTime)
    {
        Debug.Log("coroutine begin to count delay time " + delayTime);
        yield return new WaitForSecondsRealtime(delayTime);

        Debug.Log("coroutine is running");
        audioSource.enabled = false;
        audioSource.enabled = true;
    }

    public IEnumerator ChangeCurrentMelodyString(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        audioManager.currentMelody = audioSourceKey;
    }

    public IEnumerator ChangeCurrentAccompanyString(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        audioManager.currentAccompany = audioSourceKey;
    }

}
