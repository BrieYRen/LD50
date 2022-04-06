using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level1 : Level
{
    [Header("Inventory Items")]

    [SerializeField]
    Item[] conditionItems;

    [SerializeField]
    Item[] spawnItems;

    Inventory inventory;

    [Header("Animations")]

    [SerializeField]
    CanvasGroup animCanvasGroup;

    int currentStage = 0;

    ToggleHUD toggleHUD;
    LevelManager levelManager;

    [Header("First Anim")]

    [SerializeField]
    AnimFrames animStartCat;

    [SerializeField]
    AnimFrames animStartBird;

    [SerializeField]
    TextMeshProUGUI themeText;

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

    string toyFSMID = "Toy";
    string toyMusicStateID = "ToyMusic";
    
    [Header("S1 Failed Anim")]

    [SerializeField]
    AnimFrames s1FailedBirdAnim;

    [SerializeField]
    AnimFrames s1FailedCatAnim;

    const float s1FailedAnimTime = 4f;

    const int s1FailedStartFrame = 0;
    const int s1FailedEndFrame = 19;

    [Header("S2 Result Conditions")]

    [SerializeField]
    CanvasGroup cageFSMCanvasGroup;

    [Header("S2 Diary Close Anim")]

    [SerializeField]
    AnimFrames s2closeAnimCat;
    AnimFrames s2closeAnimBird;

    const float s2closeAnimTime = 6f;

    const int s2closeStartFrame = 0;
    const int s2FailedEndFrame = 28;


    private void Start()
    {
        toggleHUD = GameManager.instance.toggleHUD;

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
            }
        }      

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

    void PlayStartAnim()
    {       
        toggleHUD.HideHUD();
        animCanvasGroup.blocksRaycasts = true;

        animStartCat.PlayOnce(startFrame, endFrame,firstRate);
        animStartBird.PlayOnce(startFrame, endFrame, firstRate);
        
        StartCoroutine(ResumeHUDInSec(firstAnimTime));
        StartCoroutine(AllowInteract(firstAnimTime));

        StartCoroutine(SetAnimToIndex(animStartCat, frozeFrame, firstAnimTime));
        StartCoroutine(SetAnimToIndex(animStartBird, frozeFrameBird, firstAnimTime));

        StartCoroutine(ShowText(tutText, firstAnimTime - textOffsetTime * 1.5f, textOffsetTime * 1.8f));
    }

    void PlayS1FailedAnim()
    {
        toggleHUD.HideHUD();
        animCanvasGroup.blocksRaycasts = true;

        animStartBird.gameObject.SetActive(false);
        animStartCat.gameObject.SetActive(false);
        s1FailedBirdAnim.gameObject.SetActive(true);
        s1FailedCatAnim.gameObject.SetActive(true);

        s1FailedBirdAnim.PlayOnce(s1FailedStartFrame, s1FailedEndFrame, firstRate);
        s1FailedCatAnim.PlayOnce(s1FailedStartFrame, s1FailedEndFrame, firstRate);

        StartCoroutine(ResumeHUDInSec(s1FailedAnimTime));
        StartCoroutine(AllowInteract(s1FailedAnimTime));

        StartCoroutine(SetAnimToIndex(s1FailedCatAnim, s1FailedStartFrame, s1FailedAnimTime));
        StartCoroutine(SetAnimToIndex(s1FailedBirdAnim, s1FailedStartFrame, s1FailedAnimTime));
    }

    void PlayS1WinAnim()
    {
        Debug.Log("play toy anim");
    }

    void PlayS2DiaryCloseAnim()
    {
        toggleHUD.HideHUD();
        animCanvasGroup.blocksRaycasts = true;

        cageFSMCanvasGroup.alpha = 0f;
        // to do : hide all previous anims
        s2closeAnimBird.gameObject.SetActive(true);
        s2closeAnimCat.gameObject.SetActive(true);

        s2closeAnimBird.PlayOnce(s2closeStartFrame, s2FailedEndFrame, firstRate);
        s2closeAnimCat.PlayOnce(s2closeStartFrame, s2FailedEndFrame, firstRate);

        StartCoroutine(ResumeHUDInSec(s2closeAnimTime));
        StartCoroutine(AllowInteract(s2closeAnimTime));

        StartCoroutine(SetActiveInSec(s2closeAnimBird.gameObject, false, s2closeAnimTime));
        StartCoroutine(SetActiveInSec(s2closeAnimCat.gameObject, false, s2closeAnimTime));

        // to do : show all previous anims

        StartCoroutine(SetCanvasAlphaInSec(cageFSMCanvasGroup, 1f, s2closeAnimTime));
    }

    void PlayS2DiaryOpenAnim()
    {

    }

    void PlayS2WinAnim()
    {

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
        /*else if (currentStage == 1 && cageStateMachine.)
            currentStage = 2;*/

        switch (currentStage)
        {
            case 0:
                PlayS1FailedAnim();
                break;
            case 1:
                PlayS1WinAnim();
                break;

        }
    }

}
