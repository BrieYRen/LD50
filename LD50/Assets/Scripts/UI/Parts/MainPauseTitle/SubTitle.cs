using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTitle : MonoBehaviour
{
    SceneLoader sceneLoader;
    ToggleHUD toggleHUD;

    private void Start()
    {
        sceneLoader = GameManager.instance.sceneLoader;
        toggleHUD = GameManager.instance.toggleHUD;
        HideHUD();
    }

    void HideHUD()
    {
        toggleHUD.HideHUD();
    }

    void ResumeHUD()
    {
        toggleHUD.ShowHUD();
    }

    public void OnClickStartLevel()
    {
        sceneLoader.LoadNextScene();
        StartCoroutine(ResumeHUDInSec(sceneLoader.fadeDuration));
    }

    IEnumerator ResumeHUDInSec(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);

        ResumeHUD();
    }
}
