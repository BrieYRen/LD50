using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is a derived class from the base class State for one state of a Pillow state machine in level 1
/// </summary>
public class PillowUpState : State
{
    [SerializeField]
    Image normalImage;

    [SerializeField]
    Image upImage;

    [SerializeField]
    GameObject diaryKeyGO;

    [SerializeField]
    Item diaryKeyItem;

    Inventory inventory;
    AudioManager audioManager;

    const string pillowSfxName = "Pillow";


    private void Start()
    {
        inventory = GameManager.instance.inventoryManager;
        audioManager = GameManager.instance.audioManager;
    }

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        upImage.enabled = true;
        normalImage.enabled = false;

        audioManager.PlayIfHasAudio(pillowSfxName, 0f);

        if (!inventory.items.Contains(diaryKeyItem))
            diaryKeyGO.SetActive(true);
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        upImage.enabled = false;
        normalImage.enabled = true;

        audioManager.PlayIfHasAudio(pillowSfxName, 0f);

        diaryKeyGO.SetActive(false);
    }

}
