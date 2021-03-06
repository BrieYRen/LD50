using System.Collections;
using UnityEngine;

/// <summary>
/// this script will attack on the title panel
/// when player press anykey, they'll exit the title scene and enter the main menu, at the same time, a scene according to player's previous progression is loaded in advance
/// </summary>
public class AnykeyToStart : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Drag and drop the title panel of the game here, it will be destroy after exit the title scene")]
    GameObject titlePanel;

    [SerializeField]
    [Tooltip("Drag and drop the continue button in main menu here, when there is previous progression in save file, it will be activated")]
    GameObject continueButton;

    [SerializeField]
    AnimFrames animFrames;

    int sceneIndexToShowContinue = 2;

    SceneLoader sceneLoader;

    int sceneIndexToLoad = 1;
    bool isLoading = false;

    AudioManager audioManager;
    string bgmMelodyName = "BGMIntroMelody";
    string bgmAccompanyName = "BGMIntroAccompany";


    private void Start()
    {
        sceneLoader = GameManager.instance.sceneLoader;

        if (PlayerPrefs.HasKey(sceneLoader.sceneIndexSaveKey))
        {
            sceneIndexToLoad = PlayerPrefs.GetInt(sceneLoader.sceneIndexSaveKey);

            if (sceneIndexToLoad > sceneIndexToShowContinue)
                continueButton.SetActive(true);
        }

        audioManager = GameManager.instance.audioManager;
        StartCoroutine(PlayBGM(.01f));

        animFrames.AlwaysPlay(0, 5, 2f);
    }

    IEnumerator PlayBGM(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        audioManager.PlayIfHasTwoLayerMusic(bgmMelodyName, bgmAccompanyName, true, 0);
    }


    private void Update()
    {
        if (isLoading)
            return;

        if (Input.anyKey)
        {
            isLoading = true;
            sceneLoader.LoadCertainScene(sceneIndexToLoad);
            Destroy(titlePanel, sceneLoader.fadeDuration);
         
        }
                    
    }

    
}
