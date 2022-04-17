using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is a derived class from the base class State for a state of a LaserPen state machine in level 2
/// </summary>
public class LaserOnState : State
{
    [SerializeField]
    Image laserOnImage;

    [SerializeField]
    Image laserOffImage;

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        laserOnImage.enabled = true;
        laserOffImage.enabled = false;
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        laserOffImage.enabled = true;
        laserOnImage.enabled = false;
    }
}
