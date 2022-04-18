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

    AudioManager audioManager;

    const string laserSwitchSfxName = "LaserPen";


    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
    }


    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        laserOnImage.enabled = true;
        laserOffImage.enabled = false;

        audioManager.PlayIfHasAudio(laserSwitchSfxName, 0f);
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        laserOffImage.enabled = true;
        laserOnImage.enabled = false;

        audioManager.PlayIfHasAudio(laserSwitchSfxName, 0f);
    }
}
