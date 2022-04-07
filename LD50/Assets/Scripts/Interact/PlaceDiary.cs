using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    string cageAreaTag = "";

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

        cagePlaceArea.blocksRaycasts = false;

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
            }              
            else if (item == diaryClose)
            {
                hasPlaced = true;
                cageStateMachine.ChangeCurrentState(cageDiaryClose);
                RemoveItem();
            }               
        }      
    }

    void RemoveItem()
    {
        // remove item from inventory
        inventory.Removed(item);
    }
}
