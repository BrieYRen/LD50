using UnityEngine;
using UnityEngine.UI;

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
