using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is a derived class from the base class State for the three states of a TV state machine in level 2
/// </summary>
public class TVState : State
{
    [SerializeField]
    Image tvControlImage;

    [SerializeField]
    Image tvScreenImage;


    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        tvControlImage.enabled = true;
        tvScreenImage.enabled = true;
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        tvControlImage.enabled = false;
        tvScreenImage.enabled = false;
    }
}
