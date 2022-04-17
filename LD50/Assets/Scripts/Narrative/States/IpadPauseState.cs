using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is a derived class from the base class State for a state of a Ipad state machine in level 3
/// </summary>
public class IpadPauseState : State
{
    [SerializeField]
    Image pauseImage;

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        pauseImage.enabled = true;
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        pauseImage.enabled = false;
    }
}
