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


    private void Start()
    {
        inventory = GameManager.instance.inventoryManager;
    }

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        upImage.enabled = true;
        normalImage.enabled = false;

        if (!inventory.items.Contains(diaryKeyItem))
            diaryKeyGO.SetActive(true);
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        upImage.enabled = false;
        normalImage.enabled = true;

        diaryKeyGO.SetActive(false);
    }

}
