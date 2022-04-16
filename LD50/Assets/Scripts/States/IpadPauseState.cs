using UnityEngine;
using UnityEngine.UI;

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
