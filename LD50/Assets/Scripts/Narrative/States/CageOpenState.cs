using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// this is a derived class from the base class State for a state of the Cage state machine in level 1
/// </summary>
public class CageOpenState : State
{
    [Header("Specific Settings")]

    [SerializeField]
    Image image;

    private void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }


    public override void DoWhenEnter()
    {
        base.DoWhenEnter();

        image.enabled = true;
    }

    public override void DoWhenExit()
    {
        base.DoWhenExit();

        image.enabled = false;
    }

    void OnSceneUnloaded(Scene scene)
    {
        stateMachine.gameObject.SetActive(false);
    }
}
