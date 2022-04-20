using System.Collections;
using UnityEngine;

/// <summary>
/// script to be attach to each level's sub-title
/// </summary>
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

    /// <summary>
    /// to be addlistener to the load button
    /// </summary>
    public void OnClickStartLevel()
    {
        sceneLoader.LoadNextScene();
        //StartCoroutine(ResumeHUDInSec(sceneLoader.fadeDuration));
    }

    IEnumerator ResumeHUDInSec(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);

        ResumeHUD();
    }
}
