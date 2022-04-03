using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyMusicState : State
{
    [SerializeField]
    string toyMusicName;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
    }

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        if (toyMusicName != null)
            audioManager.PlayIfHasAudio(toyMusicName, 1);
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        if (toyMusicName != null)
            audioManager.StopPlayCertainAudio(toyMusicName, 1);
    }

    public override void DoInState()
    {
        base.DoInState();
    }
}
