using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is the base class of state machine, should be attach to an object which is the parent object of objects attached with state scripts
/// </summary>
public class StateMachine : MonoBehaviour
{
    /// <summary>
    /// the id of this state machine, used for identify
    /// </summary>
    [SerializeField]
    protected string stateMachineID;
    public string StateMachineID
    {
        get
        {
            return stateMachineID;
        }
    }

    /// <summary>
    /// is the state machine currently running?
    /// </summary>
    protected bool isActive = false;

    /// <summary>
    /// states this state machine contains
    /// </summary>
    protected List<State> states = new List<State>();

    /// <summary>
    /// the current state of this state machine
    /// </summary>
    protected State currentState;
    public State CurrentState
    {
        get
        {
            return currentState;
        }
    }

    /// <summary>
    /// the transition conditions of the current state
    /// </summary>
    protected List<StateTransition> currentStateTransitions = new List<StateTransition>();


    private void Start()
    {
        State[] childStates = GetComponentsInChildren<State>();

        if (childStates == null)
            return;

        for (int i = 0; i < childStates.Length; i++)
        {
            AddState(childStates[i]);
        }
    }

    private void Update()
    {
        if (isActive)
        {
            currentState.DoInState();

            if (currentState.AlwaysCheckConditions)
            {
                for (int i = 0; i < currentStateTransitions.Count; i++)
                {
                    currentStateTransitions[i].TransitionCondition();
                }
            }
                       
        }

    }

    /// <summary>
    /// public method to add a state to this state machine
    /// </summary>
    /// <param name="state"></param>
    public void AddState(State state)
    {
        if (states == null)
            states = new List<State>();

        if (states.Contains(state))
            return;
            
        states.Add(state);

        if (states.Count == 1)
        {          
            ChangeCurrentState(state);
            InitStateMachine();
        }
            
    }

    /// <summary>
    /// public method to remove an existing state from this state machine
    /// </summary>
    /// <param name="state"></param>
    public void RemoveState(State state)
    {
        if (states == null)
            return;

        if (!states.Contains(state))
            return;

        if (isActive && state == currentState)
        {
            Debug.Log("Can't remove a state when it's the current state of a running state machine");
            return;
        }

        states.Remove(state);
    }

    /// <summary>
    /// public method to begin to run the state machine
    /// </summary>
    public void InitStateMachine()
    {
        if (states != null && currentState != null && (currentStateTransitions != null || !currentState.AlwaysCheckConditions))
            isActive = true;
    }

    /// <summary>
    /// public method to stop running the state machine
    /// </summary>
    public void StopStateMachine()
    {
        isActive = false;
    }

    /// <summary>
    /// public method to change current state
    /// </summary>
    /// <param name="state"></param>
    public void ChangeCurrentState(State state)
    {
        if (currentState == state)
            return;

        if (!states.Contains(state))
            return;

        if (state == null || (state.Transitions == null && state.AlwaysCheckConditions))
            return;

        if (currentState != null)
        {
            currentState.DoWhenExit();

            state.DoWhenEnter();
        }
            
        currentState = state;
        currentStateTransitions = state.Transitions;
        
    }

    /// <summary>
    /// public method to change current state to next state
    /// if it's already the last state, then change to the first state
    /// </summary>
    public void ChangeToNextState()
    {
        if (currentState == null || states.Count == 0)
            return;

        int currentIndex = states.IndexOf(currentState);
        int targetIndex = currentIndex == states.Count - 1 ? 0 : currentIndex + 1;
        State targetState = states[targetIndex];

        ChangeCurrentState(targetState);
    }

}
