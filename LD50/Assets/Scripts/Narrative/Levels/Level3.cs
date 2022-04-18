using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// throwaway code for level three's specific narrative requirements
/// </summary>
public class Level3 : Level
{
    [Header("Animations")]

    [SerializeField]
    CanvasGroup animCanvasGroup;

    Canvas canvasOverride;

    [SerializeField]
    AnimFrames[] allAnimFrames;

    const int defaultSortLayer = 0;
    const int playOnTopSortLayer = 20;

    int currentStage = 0;

    [SerializeField]
    UIPanel[] detailPanles;

    ToggleHUD toggleHUD;
    BlockPanel blockPanel;
    InventoryPanel inventoryPanel;

    LevelManager levelManager;
    SceneLoader sceneLoader;

    [Header("S1 Start Anim")]
   
    [SerializeField]
    AnimFrames[] activedStartAnims;

    [SerializeField]
    AnimFrames[] animatedStartAnims;

    const float animRateCat = 4f;

    const float s1StartAnimTime = 10f; 

    const int s1StartStartFrame = 0; 
    const int s1StartEndFrame = 37; 
    const int s1StartFrozeFrame = 0; 

    [Header("S1 Win Anim")]

    [SerializeField]
    StateMachine ipadStateMachine;

    const string ipadPlayFSMID = "IpadPlay";

    [SerializeField]
    AnimFrames[] activedWinAnims;

    [SerializeField]
    AnimFrames[] animatedWinAnims;

    const float s1WinAnimTime = 10f;

    const int s1WinStartFrame = 0;
    const int s1WinEndFrame = 38;
    const int s1WinFrozeFrame = 38;

    [Header("Audio")]

    AudioManager audioManager;

    const string introMelodyName = "BGMIntroMelody";
    const string introAccompanyName = "BGMIntroAccompany";
    const string themeMelodyName = "BGMThemeMelody";
    const string themeAccompanyName = "BGMThemeAccompany";

    const string newsPodcastName = "NewsPodcast";

    const int introDelayBars = 0;
    const int themeDelayBars = 0;



    private void Start()
    {
        levelManager = GameManager.instance.levelManager;
        levelManager.InitCurrentLevel(this);

        toggleHUD = GameManager.instance.toggleHUD;
        blockPanel = GameManager.instance.blockPanel;
        inventoryPanel = GameManager.instance.inventoryPanel;
        canvasOverride = animCanvasGroup.gameObject.GetComponent<Canvas>();

        sceneLoader = GameManager.instance.sceneLoader;
        audioManager = GameManager.instance.audioManager;

        Invoke("PlayFirstAnim", 1f);
    }


    void UISettingsBeforeAnim()
    {
        toggleHUD.HideHUD();
        inventoryPanel.Close();

        for (int i = 0; i < detailPanles.Length; i++)
            detailPanles[i].Close();

        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();

        canvasOverride.overrideSorting = true;
        canvasOverride.sortingOrder = playOnTopSortLayer;
    }

    IEnumerator UISettingAfterAnim(float animTime)
    {
        yield return new WaitForSecondsRealtime(animTime);

        toggleHUD.ShowHUD();
        blockPanel.gameObject.SetActive(false);

        canvasOverride.sortingOrder = defaultSortLayer;
        canvasOverride.overrideSorting = false;
    }

    void ActivateCertainAnims(AnimFrames[] toActivateAnims)
    {
        for (int i = 0; i < allAnimFrames.Length; i++)
            allAnimFrames[i].gameObject.SetActive(false);

        for (int i = 0; i < toActivateAnims.Length; i++)
            toActivateAnims[i].gameObject.SetActive(true);
    }

    void PlayCertainAnims(AnimFrames[] toPlayAnims, int startFrame, int endFrame, float animRate)
    {
        for (int i = 0; i < toPlayAnims.Length; i++)
            toPlayAnims[i].PlayOnce(startFrame, endFrame, animRate);
    }

    IEnumerator FrozeAtCertainFrame(AnimFrames[] toFrozeAnims, int frozeFrame, float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        for (int i = 0; i < toFrozeAnims.Length; i++)
            toFrozeAnims[i].GetComponent<Image>().sprite = toFrozeAnims[i].sprites[frozeFrame];
    }

    IEnumerator StopPlayingNews(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        audioManager.StopPlayCertainAudio(newsPodcastName, .2f);
    }

    IEnumerator LoadNextScene(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        toggleHUD.HideHUD();
        sceneLoader.LoadNextScene();
    }


    void PlayFirstAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(themeMelodyName, themeAccompanyName, false, themeDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedStartAnims);

        PlayCertainAnims(animatedStartAnims, s1StartStartFrame, s1StartEndFrame, animRateCat);

        StartCoroutine(FrozeAtCertainFrame(animatedStartAnims, s1StartFrozeFrame, s1StartAnimTime));
        
        StartCoroutine(UISettingAfterAnim(s1StartAnimTime));
    }

    void PlayWinAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(introMelodyName, introAccompanyName, true, introDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedWinAnims);

        PlayCertainAnims(animatedWinAnims, s1WinStartFrame, s1WinEndFrame, animRateCat);

        StartCoroutine(StopPlayingNews(s1WinAnimTime - 3f));
        StartCoroutine(FrozeAtCertainFrame(animatedWinAnims, s1WinFrozeFrame, s1WinAnimTime));
        StartCoroutine(UISettingAfterAnim(s1WinAnimTime + 2f));

        StartCoroutine(LoadNextScene(s1WinAnimTime + 2f));
    }


    public override void CheckAnimConditions()
    {
        base.CheckAnimConditions();

        if (currentStage == 0 && ipadStateMachine.CurrentState.StateID == ipadPlayFSMID)
            currentStage = 1;

        switch (currentStage)
        {
            case 0:
                PlayFirstAnim();
                break;
            case 1:
                PlayWinAnim();
                break;
        }
    }

}
