using UnityEngine;
using UnityEngine.UI;

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
