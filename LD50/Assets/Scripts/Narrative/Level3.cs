using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3 : Level
{
    [Header("Animations")]

    [SerializeField]
    CanvasGroup animCanvasGroup;

    Canvas canvasOverride;

    [SerializeField]
    AnimFrames[] allAnimFrames;

    const int defaultSortLayer = 1;
    const int playOnTopSortLayer = 20;

    int currentStage = 0;

    [SerializeField]
    UIPanel[] detailPanles;

    [SerializeField]
    Image papernCupBG;

    ToggleHUD toggleHUD;
    BlockPanel blockPanel;
    InventoryPanel inventoryPanel;

    LevelManager levelManager;
    AudioManager audioManager;
    SceneLoader sceneLoader;

    [Header("S1 Start Anim")]
   
    [SerializeField]
    AnimFrames[] activedStartAnims;

    [SerializeField]
    AnimFrames[] animatedStartAnims;

    const float animRateCat = 5f;

    const float s1StartAnimTime = .1f; //todo

    const int s1StartStartFrame = 0; //todo
    const int s1StartEndFrame = 0; //todo
    const int s1StartFrozeFrame = 0; //todo

    [Header("S1 Failed Anim")]

    [SerializeField]
    StateMachine ipadStateMachine;

    const string ipadPlayFSMID = "IpadPlay";



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

        for (int i = 0; i < detailPanles.Length; i++)
            detailPanles[i].Close();

        papernCupBG.enabled = false;

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

        papernCupBG.enabled = true;
    }


    void PlayFirstAnim()
    {
        UISettingsBeforeAnim();

        //todo

        StartCoroutine(UISettingAfterAnim(s1StartAnimTime));
    }

    public override void CheckAnimConditions()
    {
        base.CheckAnimConditions();

        //todo

        switch (currentStage)
        {
            case 0:
                PlayFirstAnim();
                break;
            //todo
        }
    }

}
