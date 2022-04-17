using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is a derived class from the base class State for a state of the Toy state machine in level 1
/// </summary>
public class ToyMusicState : State
{
    [SerializeField]
    string toyMusicName;

    [SerializeField]
    Image silentImage;

    [SerializeField]
    Image musicImage;

    AudioManager audioManager;


    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
    }

    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        if (toyMusicName != null)
        {
            audioManager.PlayIfHasAudio(toyMusicName, 1);

            silentImage.enabled = false;
            musicImage.enabled = true;
        }
            
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        if (toyMusicName != null)
        {
            audioManager.StopPlayCertainAudio(toyMusicName, 1);

            silentImage.enabled = true;
            musicImage.enabled = false;
        }
            
    }

}
