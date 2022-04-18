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

    [SerializeField]
    string tvAMBName;

    AudioManager audioManager;

    const string tvControlSfxName = "TVControl";


    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
    }


    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        tvControlImage.enabled = true;
        tvScreenImage.enabled = true;

        audioManager.PlayIfHasAudio(tvControlSfxName, 0f);
        audioManager.PlayIfHasAudio(tvAMBName, 1f);
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        tvControlImage.enabled = false;
        tvScreenImage.enabled = false;

        audioManager.StopPlayCertainAudio(tvAMBName, 1f);
    }
}
