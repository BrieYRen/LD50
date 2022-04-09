using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// throw away code for level one's specific narrative requirements
/// </summary>
public class Level1 : Level
{
    [Header("Inventory Items")]

    [SerializeField]
    Item[] conditionItems;

    [SerializeField]
    Item[] spawnItems;

    Inventory inventory;

    InventoryPanel inventoryPanel;

    [Header("Animations")]

    [SerializeField]
    CanvasGroup animCanvasGroup;

    Canvas canvasOverride;

    AnimFrames[] allAnimFrames;

    const int defaultSortLayer = 0;
    const int playOnTopSortLayer = 20;

    int currentStage = 0;

    ToggleHUD toggleHUD;
    BlockPanel blockPanel;
    LevelManager levelManager;

    [Header("First Anim")]

    [SerializeField]
    AnimFrames animStartCat;

    [SerializeField]
    AnimFrames animStartBird;

    
    [SerializeField]
    TextMeshProUGUI tutText;

    const float firstRate = 5;

    const int startFrame = 0;
    const int endFrame = 35;
    const int frozeFrame = 17;  
    
    const int frozeFrameBird = 0;

    const float firstAnimTime = 7.5f;
    const float textOffsetTime = 1f;

    [Header("S1 Result Conditions")]
    
    [SerializeField]
    Item toyItem;

    StateMachine toyStateMachine;

    const string toyFSMID = "Toy";
    const string toyMusicStateID = "ToyMusic";
    
    [Header("S1 Failed Anim")]

    [SerializeField]
    AnimFrames s1FailedBirdAnim;

    [SerializeField]
    AnimFrames s1FailedCatAnim;

    const float s1FailedAnimTime = 4f;

    const int s1FailedStartFrame = 0;
    const int s1FailedEndFrame = 19;

    [Header("S1 Win Anim")]

    StateMachine cageStateMachine;
    const string cageFSMID = "Cage";

    [SerializeField]
    AnimFrames s1WinCatAnim;

    [SerializeField]
    AnimFrames s1WinBirdAnim;

    const float s1WinAnimTime = 9.5f;

    const int s1WinStartFrame = 0;
    const int s1WinEndFrame = 45;
    const int s1WinFrozeFrame = 10;

    [Header("S2 Result Conditions")]

    CanvasGroup cageFSMCanvasGroup;

    const string cageNormalStateID = "CageNormal";
    const string cageDiaryCloseStateID = "CageDiaryClose";
    const string cageDiaryOpenStateID = "CageDiaryOpen";
    const string cageOpenStateID = "CageOpen";

    [Header("S2 Normal Anim")]

    [SerializeField]
    AnimFrames s2NormalAnimBird;

    [SerializeField]
    AnimFrames s2NormalAnimCat;

    const float s2NormalAnimTime = 7.5f;

    const int s2NormalStartFrame = 0;
    const int s2NormalEndFrame = 35; 
    const int s2NormalFrozeFrame = 0;

    [Header("S2 Diary Close Anim")]

    [SerializeField]
    AnimFrames s2closeAnimCat;

    [SerializeField]
    AnimFrames s2closeAnimBird;

    const float s2closeAnimTime = 6f;

    const int s2closeStartFrame = 0;
    const int s2closeEndFrame = 28;
    const int s2closeFrozeFrame = 0;

    [Header("S2 Diary Open Anim")]

    [SerializeField]
    AnimFrames s2DiaryOpenAnimCat;

    [SerializeField]
    AnimFrames s2DiaryOpenAnimBird;

    const float s2DiaryOpenAnimTime = 8f;

    const int s2DiaryOpenStartFrame = 0;
    const int s2DiaryOpenEndFrame = 36; 
    const int s2DiaryOpenFrozeFrame = 0; 

    [Header("S2 Win Anim")]

    [SerializeField]
    AnimFrames s2WinAnimCat;

    [SerializeField]
    AnimFrames s2WinAnimBird;

    const float s2WinAnimTime = 3f; 

    const int s2WinStartFrame = 0;
    const int s2WinEndFrame = 15; 
    const int s2WinFrozeFrame = 15; 

    [SerializeField]
    TextMeshProUGUI themeText;

    SceneLoader sceneLoader;

    [Header("Music and Sound")]

    AudioManager audioManager;

    //string titleName = "BGMTitle";
    //string mainLoopName = "BGMMain";
    string introName = "BGMIntro";
    //float introTime = 16f;
    string themeName = "BGMTheme";
    //float themeTime = 16f;

    string toyMusic = "Toy";


    private void Start()
    {
        toggleHUD = GameManager.instance.toggleHUD;
        inventoryPanel = GameManager.instance.inventoryPanel;
        sceneLoader = GameManager.instance.sceneLoader;
        blockPanel = GameManager.instance.blockPanel;

        levelManager = GameManager.instance.levelManager;
        levelManager.InitCurrentLevel(this);

        inventory = GameManager.instance.inventoryManager;
        inventory.onItemChangedCallback += CheckItem;

        StateMachine[] stateMachines = GameManager.instance.stateMachines;
        if (stateMachines.Length > 0)
        {
            for (int i = 0; i < stateMachines.Length; i++)
            {
                if (stateMachines[i].StateMachineID == toyFSMID)
                    toyStateMachine = stateMachines[i];

                if (stateMachines[i].StateMachineID == cageFSMID)
                    cageStateMachine = stateMachines[i];
            }
        }

        canvasOverride = animCanvasGroup.gameObject.GetComponent<Canvas>();
        allAnimFrames = animCanvasGroup.gameObject.GetComponentsInChildren<AnimFrames>();

        cageFSMCanvasGroup = cageStateMachine.gameObject.GetComponent<CanvasGroup>();

        audioManager = GameManager.instance.audioManager;

        PlayStartAnim();
    }

    void CheckItem()
    {
        for (int i = 0; i < conditionItems.Length; i++)
        {
            if (inventory.items.Contains(conditionItems[i]) && !inventory.items.Contains(spawnItems[i]))
                inventory.Add(spawnItems[i]);
        }
    }


    IEnumerator ResumeHUDInSec(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        toggleHUD.ShowHUD();
    }

    IEnumerator SetAnimToIndex(AnimFrames anim, int index, float waitSec)
    {
        yield return new WaitForSecondsRealtime(waitSec);

        anim.GetComponent<Image>().sprite = anim.sprites[index];
    }

    IEnumerator ShowText(TextMeshProUGUI text, float delayTime, float showTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        text.enabled = true;

        yield return new WaitForSecondsRealtime(showTime);
        text.enabled = false;
    }

    IEnumerator AllowInteract(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        animCanvasGroup.blocksRaycasts = false;
    }

    IEnumerator SetActiveInSec(GameObject gameObject, bool targetBool, float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        gameObject.SetActive(targetBool);
    }

    IEnumerator SetCanvasAlphaInSec(CanvasGroup canvasGroup, float targetAlpha, float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        canvasGroup.alpha = targetAlpha;
    }

    IEnumerator SetSortLayerInSec(int targetIndex, float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        canvasOverride.sortingOrder = targetIndex;
    }

    IEnumerator LoadNextSceneInSec(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        sceneLoader.LoadNextScene();
    }

    IEnumerator HideUiWhenUnload(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        RectTransform[] toHideRTs = cageStateMachine.GetComponentInParent<RectTransform>().GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < toHideRTs.Length; i++)
            toHideRTs[i].gameObject.SetActive(false);

        inventoryPanel.Close();

        audioManager.StopPlayCertainAudio(toyMusic, 1f);
    }


    void PlayStartAnim()
    {
        audioManager.PlayIfHasMusic(introName);

        toggleHUD.HideHUD();
        inventoryPanel.Close();
        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();
        animCanvasGroup.blocksRaycasts = true;

        for (int i = 0; i < allAnimFrames.Length; i++)
            allAnimFrames[i].gameObject.SetActive(false);

        animStartBird.gameObject.SetActive(true);
        animStartCat.gameObject.SetActive(true);

        animStartCat.PlayOnce(startFrame, endFrame,firstRate);
        animStartBird.PlayOnce(startFrame, endFrame, firstRate);
        
        StartCoroutine(ResumeHUDInSec(firstAnimTime));
        StartCoroutine(AllowInteract(firstAnimTime));
        StartCoroutine(SetActiveInSec(blockPanel.gameObject, false, firstAnimTime));

        StartCoroutine(SetAnimToIndex(animStartCat, frozeFrame, firstAnimTime));
        StartCoroutine(SetAnimToIndex(animStartBird, frozeFrameBird, firstAnimTime));

        StartCoroutine(ShowText(tutText, firstAnimTime - textOffsetTime * 1.5f, textOffsetTime * 3f));
    }

    void PlayS1FailedAnim()
    {
        audioManager.PlayIfHasMusic(themeName);

        toggleHUD.HideHUD();
        inventoryPanel.Close();
        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();
        animCanvasGroup.blocksRaycasts = true;
        canvasOverride.sortingOrder = playOnTopSortLayer;

        for (int i = 0; i < allAnimFrames.Length; i++)
            allAnimFrames[i].gameObject.SetActive(false);
        
        s1FailedBirdAnim.gameObject.SetActive(true);
        s1FailedCatAnim.gameObject.SetActive(true);

        s1FailedBirdAnim.PlayOnce(s1FailedStartFrame, s1FailedEndFrame, firstRate);
        s1FailedCatAnim.PlayOnce(s1FailedStartFrame, s1FailedEndFrame, firstRate);

        StartCoroutine(ResumeHUDInSec(s1FailedAnimTime));
        StartCoroutine(SetActiveInSec(blockPanel.gameObject, false, s1FailedAnimTime));
        StartCoroutine(AllowInteract(s1FailedAnimTime));
        StartCoroutine(SetSortLayerInSec(defaultSortLayer, s1FailedAnimTime));

        StartCoroutine(SetAnimToIndex(s1FailedCatAnim, s1FailedStartFrame, s1FailedAnimTime));
        StartCoroutine(SetAnimToIndex(s1FailedBirdAnim, s1FailedStartFrame, s1FailedAnimTime));
    }

    void PlayS1WinAnim()
    {
        audioManager.PlayIfHasMusic(themeName);

        toggleHUD.HideHUD();
        inventoryPanel.Close();
        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();
        animCanvasGroup.blocksRaycasts = true;
        canvasOverride.sortingOrder = playOnTopSortLayer;

        for (int i = 0; i < allAnimFrames.Length; i++)
            allAnimFrames[i].gameObject.SetActive(false);

        s1WinBirdAnim.gameObject.SetActive(true);
        s1WinCatAnim.gameObject.SetActive(true);

        s1WinBirdAnim.PlayOnce(s1WinStartFrame, s1WinEndFrame, firstRate);
        s1WinCatAnim.PlayOnce(s1WinStartFrame, s1WinEndFrame, firstRate);

        StartCoroutine(ResumeHUDInSec(s1WinAnimTime));
        StartCoroutine(SetActiveInSec(blockPanel.gameObject, false, s1WinAnimTime));
        StartCoroutine(AllowInteract(s1WinAnimTime));
        StartCoroutine(SetSortLayerInSec(defaultSortLayer, s1WinAnimTime));

        StartCoroutine(SetAnimToIndex(s1WinCatAnim, s1WinFrozeFrame, s1WinAnimTime));
        StartCoroutine(SetAnimToIndex(s1WinBirdAnim, s1WinFrozeFrame, s1WinAnimTime));

        StartCoroutine(SetActiveInSec(s1WinBirdAnim.gameObject, false, s1WinAnimTime + .1f));
        StartCoroutine(SetActiveInSec(cageStateMachine.gameObject, true, s1WinAnimTime + .1f));
    }

    void PlayS2NormalAnim()
    {
        audioManager.PlayIfHasMusic(themeName);

        toggleHUD.HideHUD();
        inventoryPanel.Close();
        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();
        animCanvasGroup.blocksRaycasts = true;
        canvasOverride.sortingOrder = playOnTopSortLayer;

        cageFSMCanvasGroup.alpha = 0f;

        for (int i = 0; i < allAnimFrames.Length; i++)
            allAnimFrames[i].gameObject.SetActive(false);

        s2NormalAnimBird.gameObject.SetActive(true);
        s2NormalAnimCat.gameObject.SetActive(true);

        s2NormalAnimBird.PlayOnce(s2NormalStartFrame, s2NormalEndFrame, firstRate);
        s2NormalAnimCat.PlayOnce(s2NormalStartFrame, s2NormalEndFrame, firstRate);

        StartCoroutine(ResumeHUDInSec(s2NormalAnimTime));
        StartCoroutine(SetActiveInSec(blockPanel.gameObject, false, s2NormalAnimTime));
        StartCoroutine(AllowInteract(s2NormalAnimTime));
        StartCoroutine(SetSortLayerInSec(defaultSortLayer, s2NormalAnimTime));

        StartCoroutine(SetAnimToIndex(s2NormalAnimCat, s2NormalFrozeFrame, s2NormalAnimTime));
        StartCoroutine(SetAnimToIndex(s2NormalAnimBird, s2NormalFrozeFrame, s2NormalAnimTime));

        StartCoroutine(SetActiveInSec(s2NormalAnimBird.gameObject, false, s2NormalAnimTime + .1f));
        StartCoroutine(SetActiveInSec(cageStateMachine.gameObject, true, s2NormalAnimTime + .1f));

        StartCoroutine(SetCanvasAlphaInSec(cageFSMCanvasGroup, 1f, s2NormalAnimTime + .1f));
    }

    void PlayS2DiaryCloseAnim()
    {
        audioManager.PlayIfHasMusic(themeName);

        toggleHUD.HideHUD();
        inventoryPanel.Close();
        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();
        animCanvasGroup.blocksRaycasts = true;
        canvasOverride.sortingOrder = playOnTopSortLayer;

        cageFSMCanvasGroup.alpha = 0f;

        for (int i = 0; i < allAnimFrames.Length; i++)
            allAnimFrames[i].gameObject.SetActive(false);

        s2closeAnimBird.gameObject.SetActive(true);
        s2closeAnimCat.gameObject.SetActive(true);

        s2closeAnimBird.PlayOnce(s2closeStartFrame, s2closeEndFrame, firstRate);
        s2closeAnimCat.PlayOnce(s2closeStartFrame, s2closeEndFrame, firstRate);

        StartCoroutine(ResumeHUDInSec(s2closeAnimTime));
        StartCoroutine(SetActiveInSec(blockPanel.gameObject, false, s2closeAnimTime));
        StartCoroutine(AllowInteract(s2closeAnimTime));
        StartCoroutine(SetSortLayerInSec(defaultSortLayer, s2closeAnimTime));

        StartCoroutine(SetAnimToIndex(s2closeAnimCat, s2closeFrozeFrame, s2closeAnimTime));
        StartCoroutine(SetAnimToIndex(s2closeAnimBird, s2closeFrozeFrame, s2closeAnimTime));

        StartCoroutine(SetActiveInSec(s2closeAnimBird.gameObject, false, s2closeAnimTime + .1f));
        StartCoroutine(SetActiveInSec(cageStateMachine.gameObject, true, s2closeAnimTime + .1f));

        StartCoroutine(SetCanvasAlphaInSec(cageFSMCanvasGroup, 1f, s2closeAnimTime));
    }

    void PlayS2DiaryOpenAnim()
    {
        audioManager.PlayIfHasMusic(themeName);

        toggleHUD.HideHUD();
        inventoryPanel.Close();
        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();
        animCanvasGroup.blocksRaycasts = true;
        canvasOverride.sortingOrder = playOnTopSortLayer;

        cageFSMCanvasGroup.alpha = 0f;

        for (int i = 0; i < allAnimFrames.Length; i++)
            allAnimFrames[i].gameObject.SetActive(false);

        s2DiaryOpenAnimBird.gameObject.SetActive(true);
        s2DiaryOpenAnimCat.gameObject.SetActive(true);

        s2DiaryOpenAnimBird.PlayOnce(s2DiaryOpenStartFrame, s2DiaryOpenEndFrame, firstRate);
        s2DiaryOpenAnimCat.PlayOnce(s2DiaryOpenStartFrame, s2DiaryOpenEndFrame, firstRate);

        StartCoroutine(ResumeHUDInSec(s2DiaryOpenAnimTime));
        StartCoroutine(SetActiveInSec(blockPanel.gameObject, false, s2DiaryOpenAnimTime));
        StartCoroutine(AllowInteract(s2DiaryOpenAnimTime));
        StartCoroutine(SetSortLayerInSec(defaultSortLayer, s2DiaryOpenAnimTime));

        StartCoroutine(SetAnimToIndex(s2DiaryOpenAnimCat, s2DiaryOpenFrozeFrame, s2DiaryOpenAnimTime));
        StartCoroutine(SetAnimToIndex(s2DiaryOpenAnimBird, s2DiaryOpenFrozeFrame, s2DiaryOpenAnimTime));

        StartCoroutine(SetActiveInSec(s2DiaryOpenAnimBird.gameObject, false, s2DiaryOpenAnimTime + .1f));
        StartCoroutine(SetActiveInSec(cageStateMachine.gameObject, true, s2DiaryOpenAnimTime + .1f));

        StartCoroutine(SetCanvasAlphaInSec(cageFSMCanvasGroup, 1f, s2DiaryOpenAnimTime));
    }

    void PlayS2WinAnim()
    {
        audioManager.PlayIfHasMusic(themeName);

        // play s2 win anim
        toggleHUD.HideHUD();
        inventoryPanel.Close();
        blockPanel.gameObject.SetActive(true);
        blockPanel.GetComponent<RectTransform>().SetAsLastSibling();
        animCanvasGroup.blocksRaycasts = true;
        canvasOverride.sortingOrder = playOnTopSortLayer;

        cageFSMCanvasGroup.alpha = 0f;

        for (int i = 0; i < allAnimFrames.Length; i++)
            allAnimFrames[i].gameObject.SetActive(false);

        s2WinAnimBird.gameObject.SetActive(true);
        s2WinAnimCat.gameObject.SetActive(true);

        s2WinAnimBird.PlayOnce(s2WinStartFrame, s2WinEndFrame, firstRate);
        s2WinAnimCat.PlayOnce(s2WinStartFrame, s2WinEndFrame, firstRate);

        StartCoroutine(ResumeHUDInSec(s2WinAnimTime + 5f));
        StartCoroutine(SetActiveInSec(blockPanel.gameObject, false, s2WinAnimTime + 5f));
        StartCoroutine(AllowInteract(s2WinAnimTime + 5f));
        StartCoroutine(SetSortLayerInSec(defaultSortLayer, s2WinAnimTime + 5f));

        StartCoroutine(SetAnimToIndex(s2WinAnimCat, s2WinFrozeFrame, s2WinAnimTime));
        StartCoroutine(SetAnimToIndex(s2WinAnimBird, s2WinFrozeFrame, s2WinAnimTime));

        // hide ui before unload
        StartCoroutine(HideUiWhenUnload(s2WinAnimTime));

        // show level theme text
        StartCoroutine(ShowText(themeText, s2WinAnimTime, textOffsetTime * 5f));

        // load next scene after finished the anim
        StartCoroutine(LoadNextSceneInSec(s2WinAnimTime + 5.01f));

    }

    public override void CheckAnimConditions()
    {
        base.CheckAnimConditions();

        if (currentStage == 0)
        {
            if (!toyStateMachine.gameObject.activeSelf)
                currentStage = 0;
            else if (!inventory.items.Contains(toyItem) && toyStateMachine.CurrentState.StateID == toyMusicStateID)
                currentStage = 1;
        }
        else
        {
            if (!cageStateMachine.gameObject.activeSelf)
                currentStage = 2;
            else if (cageStateMachine.CurrentState.StateID == cageDiaryCloseStateID)
                currentStage = 3;
            else if (cageStateMachine.CurrentState.StateID == cageDiaryOpenStateID)
                currentStage = 4;
            else if (cageStateMachine.CurrentState.StateID == cageOpenStateID)
                currentStage = 5;
            else
                currentStage = 2;
        }
        
        switch (currentStage)
        {
            case 0:
                PlayS1FailedAnim();
                break;
            case 1:
                PlayS1WinAnim();
                break;
            case 2:
                PlayS2NormalAnim();
                break;
            case 3:
                PlayS2DiaryCloseAnim();
                break;
            case 4:
                PlayS2DiaryOpenAnim();
                break;
            case 5:
                PlayS2WinAnim();
                break;
        }
    }

}
