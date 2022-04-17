using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// throwaway code for level two's specific narrative requirements
/// </summary>
public class Level2 : Level
{
    #region Variables

    [Header("Animations")]

    [SerializeField]
    CanvasGroup animCanvasGroup;

    Canvas canvasOverride;

    [SerializeField]
    AnimFrames[] allAnimFrames;

    const int defaultSortLayer = 1;
    const int playOnTopSortLayer = 20;

    int currentStage = 0;

    ToggleHUD toggleHUD;
    BlockPanel blockPanel;
    InventoryPanel inventoryPanel;
    Inventory inventory;

    LevelManager levelManager;

    [Header("S1 Start Anim")]

    [SerializeField]
    StateMachine laserStateMachine;

    [SerializeField]
    AnimFrames[] activedStartAnims;

    [SerializeField]
    AnimFrames[] animatedStartAnims;

    [SerializeField]
    AnimFrames coupleTVAnims;

    const float animRateLoop = 1.667f;
    const float animRateCat = 5f;

    const float s1StartAnimTime = 8.5f; 

    const int s1StartStartFrame = 0;
    const int s1StartEndFrame = 38; 
    const int s1StartFrozeFrame = 0;

    [Header("S1 Win Anim")]

    [SerializeField]
    string laserOnID = "LaserOn";

    [SerializeField]
    AnimFrames[] activedS1Anims;

    [SerializeField]
    AnimFrames[] animatedS1Anims;

    const float s1WinAnimTime = 15.5f; 

    const int s1WinStartFrame = 0;
    const int s1WinEndFrame = 73; 
    const int s1WinFrozeFrame = 25; 

    [Header("S2 Failed Anim")]

    [SerializeField]
    StateMachine tvStateMachine;

    string tvFishID = "TVFish";
    
    [SerializeField]
    AnimFrames cafeTVAnim;

    string tvCafeID = "TVCafe";

    const float s2FailAnimTime = 10.5f;

    const int s2FailStartFrame = 25;
    const int s2FailEndFrame = 73;
    const int s2FailFrozeFrame = 25;

    [Header("S2 Win Anim")]

    [SerializeField]
    AnimFrames fishTVAnim;

    [SerializeField]
    AnimFrames[] activedS2WinAnims;

    [SerializeField]
    AnimFrames[] animatedS2WinAnims;

    const float s2WinAnimTime = 12.5f;

    const int s2WinStartFrame = 0; 
    const int s2WinEndFrame = 60; 
    const int s2WinFrozeFrame = 60; 

    SceneLoader sceneLoader;

    [Header("Audio")]

    AudioManager audioManager;

    const string introMelodyName = "BGMTitleMelody";
    const string introAccompanyName = "BGMTitleAccompany";
    const string themeMelodyName = "BGMThemeMelody";
    const string themeAccompanyName = "BGMThemeAccompany";

    const int introDelayBars = 0;
    const int themeDelayBars = 0;

    #endregion


    private void Start()
    {
        levelManager = GameManager.instance.levelManager;
        levelManager.InitCurrentLevel(this);

        toggleHUD = GameManager.instance.toggleHUD;
        blockPanel = GameManager.instance.blockPanel;
        inventoryPanel = GameManager.instance.inventoryPanel;
        canvasOverride = animCanvasGroup.gameObject.GetComponent<Canvas>();

        inventory = GameManager.instance.inventoryManager;
        while (inventory.items.Count > 0)
            inventory.Removed(inventory.items[0]);

        sceneLoader = GameManager.instance.sceneLoader;
        audioManager = GameManager.instance.audioManager;

        PlayFirstAnim();
    }


    void UISettingsBeforeAnim()
    {
        toggleHUD.HideHUD();
        inventoryPanel.Close();

        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();

        canvasOverride.sortingOrder = playOnTopSortLayer;
    }

    IEnumerator UISettingAfterAnim(float animTime)
    {
        yield return new WaitForSecondsRealtime(animTime);

        toggleHUD.ShowHUD();      
        blockPanel.gameObject.SetActive(false);
        canvasOverride.sortingOrder = defaultSortLayer;
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

    void PlayAnimLoop(AnimFrames toPlayAnim, int startFrame, int endFrame, float animRate)
    {
        if (toPlayAnim.isActiveAndEnabled)
            toPlayAnim.AlwaysPlay(startFrame, endFrame, animRate);               
    }

    IEnumerator StopPlayAnimLoop(AnimFrames toPlayAnim, float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        toPlayAnim.StopPlay();
    }

    IEnumerator LoadNextScene(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        sceneLoader.LoadNextScene();
    }

    void PlayFirstAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(introMelodyName, introAccompanyName, true, introDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedStartAnims);

        PlayCertainAnims(animatedStartAnims, s1StartStartFrame, s1StartEndFrame, animRateCat);
        PlayAnimLoop(coupleTVAnims, 0, coupleTVAnims.sprites.Length - 1, animRateLoop);

        StartCoroutine(StopPlayAnimLoop(coupleTVAnims, s1StartAnimTime));
        StartCoroutine(FrozeAtCertainFrame(animatedStartAnims, s1StartFrozeFrame, s1StartAnimTime));
      
        StartCoroutine(UISettingAfterAnim(s1StartAnimTime));
    }

    void PlayS1FailAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(themeMelodyName, themeAccompanyName, false, themeDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedStartAnims);

        PlayCertainAnims(animatedStartAnims, s1StartStartFrame, s1StartEndFrame, animRateCat);
        PlayAnimLoop(coupleTVAnims, 0, coupleTVAnims.sprites.Length - 1, animRateLoop);

        StartCoroutine(StopPlayAnimLoop(coupleTVAnims, s1StartAnimTime));
        StartCoroutine(FrozeAtCertainFrame(animatedStartAnims, s1StartFrozeFrame, s1StartAnimTime));
        StartCoroutine(UISettingAfterAnim(s1StartAnimTime));
    }

    void PlayS1WinAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(introMelodyName, introAccompanyName, true, introDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedS1Anims);

        PlayCertainAnims(animatedS1Anims, s1WinStartFrame, s1WinEndFrame, animRateCat);
        PlayAnimLoop(coupleTVAnims, 0, coupleTVAnims.sprites.Length - 1, animRateLoop);

        StartCoroutine(StopPlayAnimLoop(coupleTVAnims, s1WinAnimTime));
        StartCoroutine(FrozeAtCertainFrame(animatedS1Anims, s1WinFrozeFrame, s1WinAnimTime));
        StartCoroutine(UISettingAfterAnim(s1WinAnimTime));

        tvStateMachine.GetComponent<Button>().interactable = true;
    }

    void PlayS2FailAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(themeMelodyName, themeAccompanyName, false, themeDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedS1Anims);

        PlayCertainAnims(animatedS1Anims, s2FailStartFrame, s2FailEndFrame, animRateCat);

        AnimFrames toPlayTVAnim = tvStateMachine.CurrentState.StateID == tvCafeID ? cafeTVAnim : coupleTVAnims;
        PlayAnimLoop(toPlayTVAnim, 0, toPlayTVAnim.sprites.Length - 1, animRateLoop);

        StartCoroutine(StopPlayAnimLoop(toPlayTVAnim, s2FailAnimTime));
        StartCoroutine(FrozeAtCertainFrame(animatedS1Anims, s2FailFrozeFrame, s2FailAnimTime));
        StartCoroutine(UISettingAfterAnim(s2FailAnimTime));
    }

    void PlayS2WinAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(introMelodyName, introAccompanyName, true, introDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedS2WinAnims);

        PlayCertainAnims(animatedS2WinAnims, s2WinStartFrame, s2WinEndFrame, animRateCat);
        PlayAnimLoop(fishTVAnim, 0, fishTVAnim.sprites.Length - 1, animRateLoop);

        StartCoroutine(FrozeAtCertainFrame(animatedS2WinAnims, s2WinFrozeFrame, s2WinAnimTime));
        StartCoroutine(UISettingAfterAnim(s2WinAnimTime));

        StartCoroutine(LoadNextScene(s2WinAnimTime + 2f));
    }


    public override void CheckAnimConditions()
    {
        base.CheckAnimConditions();

        if (currentStage <= 1)
        {
            if (laserStateMachine.CurrentState.StateID == laserOnID)
                currentStage = 2;
            else
                currentStage = 1;
        }
        else
        {
            if (tvStateMachine.CurrentState.StateID != tvFishID)
                currentStage = 3;
            else
                currentStage = 4;
        }

        switch (currentStage)
        {
            case 0:
                PlayFirstAnim();
                break;
            case 1:
                PlayS1FailAnim();
                break;
            case 2:
                PlayS1WinAnim();
                break;
            case 3:
                PlayS2FailAnim();
                break;
            case 4:
                PlayS2WinAnim();
                break;
        }
    }
}
