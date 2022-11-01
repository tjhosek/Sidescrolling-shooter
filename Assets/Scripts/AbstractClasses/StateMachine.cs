using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages transitions between states
/// </summary>
public abstract class StateMachine : MonoBehaviour
{
    protected State _state;
    /// <summary>
    /// The current State of this StateMachine. Changing this value automatically calls the OnExit() and OnEnter() methods for the old and new states respectively
    /// </summary>
    /// <value>The current state</value>
    public virtual State state {
        get { return _state; }
        set {
            if(_state != null) {
                _state.OnExit(value);
            }
            State oldState = _state;
            _state = value;
            _state.OnEnter(oldState);
        }
    }
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (_state != null) {
            _state.UpdateBehavior();
        }
    }
}
