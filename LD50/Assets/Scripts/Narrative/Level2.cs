using System.Collections;
using System.Collections.Generic;
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

    LevelManager levelManager;

    [Header("S1 Start Anim")]

    [SerializeField]
    StateMachine laserStateMachine;

    [SerializeField]
    AnimFrames[] activedStartAnims;

    [SerializeField]
    AnimFrames[] animatedStartAnims;

    const float animRateCat = 5f;

    const float s1StartAnimTime = 8.5f; 

    const int s1StartStartFrame = 0;
    const int s1StartEndFrame = 38; 
    const int s1StartFrozeFrame = 0;

    [Header("S1 Win Anim")]

    const string laserOnID = "LaserOn";

    [SerializeField]
    AnimFrames[] activedS1Anims;

    [SerializeField]
    AnimFrames[] animatedS1Anims;
   
    const float s1WinAnimTime = 0f; // todo

    const int s1WinStartFrame = 0;
    const int s1WinEndFrame = 0; //todo
    const int s1WinFrozeFrame = 0; // todo

    [Header("S2 Anim")]

    [SerializeField]
    StateMachine tvStateMachine;

    SceneLoader sceneLoader;

    [Header("Audio")]

    AudioManager audioManager;

    const string introMelodyName = "BGMTitleMelody";
    const string introAccompanyName = "BGMTitleAccompany";
    const string themeMelodyName = "BGMThemeMelody";
    const string themeAccompanyName = "BGMThemeAccompany";

    const int introDelayBars = 1;
    const int themeDelayBars = 3;

    #endregion


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

    void PlayFirstAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(introMelodyName, introAccompanyName, true, introDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedStartAnims);

        PlayCertainAnims(animatedStartAnims, s1StartStartFrame, s1StartEndFrame, animRateCat);
        // todo: play tv couple

        StartCoroutine(FrozeAtCertainFrame(animatedStartAnims, s1StartFrozeFrame, s1StartAnimTime));
        StartCoroutine(UISettingAfterAnim(s1StartAnimTime));
    }

    void PlayS1FailAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(themeMelodyName, themeAccompanyName, false, themeDelayBars);

        UISettingsBeforeAnim();
        ActivateCertainAnims(activedStartAnims);

        PlayCertainAnims(animatedStartAnims, s1StartStartFrame, s1StartEndFrame, animRateCat);

        StartCoroutine(FrozeAtCertainFrame(animatedStartAnims, s1StartFrozeFrame, s1StartAnimTime));
        StartCoroutine(UISettingAfterAnim(s1StartAnimTime));
    }

    void PlayS1WinAnim()
    {
        audioManager.PlayIfHasTwoLayerMusic(themeMelodyName, themeAccompanyName, false, themeDelayBars);


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

        }

        switch (currentStage)
        {
            case 0:
                PlayFirstAnim();
                break;
            case 1:
                PlayS1FailAnim();
                break;
        }
    }
}
