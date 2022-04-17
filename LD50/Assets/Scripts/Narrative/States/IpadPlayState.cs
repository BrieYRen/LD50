using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is a derived class from the base class State for a state of a Ipad state machine in level 3
/// </summary>
public class IpadPlayState : State
{
    [SerializeField]
    Image playImage;

    [SerializeField]
    string newsAudioName;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
    }

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        playImage.enabled = true;
        audioManager.PlayIfHasAudio(newsAudioName, 1f);
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        playImage.enabled = false;
        audioManager.StopPlayCertainAudio(newsAudioName, 1f);
    }
}
