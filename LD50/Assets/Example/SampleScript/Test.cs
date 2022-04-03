using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    AudioManager audioManager;

    public string bgmName;
    public float waitSec = 1f;
    public float fadeSec = 2f;

    public KeyCode stopKeyCode = KeyCode.P;
    public KeyCode changeBgmKeyCode = KeyCode.C;
    public string altBgmName;

    public string toStopName;

    SceneLoader sceneLoader;
    public KeyCode nextLevelKeyCode = KeyCode.N;

    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
        sceneLoader = GameManager.instance.sceneLoader;
        StartCoroutine(PlayBGM());
    }

    IEnumerator PlayBGM()
    {
        yield return new WaitForSeconds(waitSec);

        audioManager.PlayIfHasAudio(bgmName,fadeSec);
    }

    private void Update()
    {
        if (Input.GetKeyDown(changeBgmKeyCode))
        {
            audioManager.PlayIfHasAudio(altBgmName, fadeSec);
            audioManager.StopPlayCertainAudio(bgmName, fadeSec);
        }

        if (Input.GetKeyDown(stopKeyCode))
        {
            audioManager.StopPlayCertainAudio(toStopName, fadeSec);
        }

        if (Input.GetKeyDown(nextLevelKeyCode))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            sceneLoader.LoadNextScene();
        }
    }



}
