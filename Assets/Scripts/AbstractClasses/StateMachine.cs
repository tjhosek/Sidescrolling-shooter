using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages transitions between states
/// </summary>
public abstract class StateMachine : MonoBehaviour
{
    protected State _state;
    public virtual State state {
        get { return state; }
        set {
            state.OnExit(value);
            State oldState = state;
            state = value;
            state.OnEnter(oldState);
        }
    }
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        state.UpdateBehavior();
    }
}
