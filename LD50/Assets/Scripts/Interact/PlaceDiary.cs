using UnityEngine;

/// <summary>
/// this is a derived class from the base class PlaceItem for placing two specific items diaryOpen and diaryClose
/// at a specific object cage state machine in level 1, use throwaway code for the time limit in Ludum Dare
/// </summary>
public class PlaceDiary : PlaceItem
{
    [Header("Diary Settings")]

    [SerializeField]
    CanvasGroup cagePlaceArea;

    [SerializeField]
    StateMachine cageStateMachine;

    [SerializeField]
    State cageDiaryOpen;

    [SerializeField]
    State cageDiaryClose;

    [SerializeField]
    State cageDoorOpen;

    [SerializeField]
    string cageAreaTag = "cageArea";

    [SerializeField]
    Item diaryOpen;

    [SerializeField]
    Item diaryClose;

    bool hasPlaced = false;


    public override void ReadyToPlace()
    {
        base.ReadyToPlace();

        cagePlaceArea.blocksRaycasts = true;
    }

    public override void BackNormal()
    {
        base.BackNormal();

        if (hasPlaced)
        {
            cagePlaceArea.blocksRaycasts = true;
            hasPlaced = false;
        }          
    }

    public override void SpecificCheck()
    {
        base.SpecificCheck();

        // check if it's in cage area
        bool canPlaceCage = RaycastTagCheck(cageAreaTag);

        // if it's in cage area and the cage door is not open
        if (canPlaceCage && cageStateMachine.CurrentState != cageDoorOpen)
        {
            // change cage state
            if (item == diaryOpen)
            {
                hasPlaced = true;
                cageStateMachine.ChangeCurrentState(cageDiaryOpen);
                RemoveItem();
                audioManager.PlayIfHasAudio(item.PlaceSfx, 0f);
            }              
            else if (item == diaryClose)
            {
                hasPlaced = true;
                cageStateMachine.ChangeCurrentState(cageDiaryClose);
                RemoveItem();
                audioManager.PlayIfHasAudio(item.PlaceSfx, 0f);
            }               
        }      
    }

    void RemoveItem()
    {
        // remove item from inventory
        inventory.Removed(item);
    }
}
