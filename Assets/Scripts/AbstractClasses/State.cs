using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what sould be done before, durring, and after a StateMachine is in a state. Transitioning between states is managed by StateMachine
/// </summary>
public abstract class State
{
    protected string _name;
    /// <summary>
    /// The name of the state. Primarily used for debugging purposes
    /// </summary>
    /// <value></value>
    public string name { 
        get { return _name; } 
        set {_name = value; } 
        }

    /// <summary>
    /// Empty constructor
    /// </summary>
    /// <returns>A newly constructed state</returns>
    public State() {
        this.name = "";
    }
    /// <summary>
    /// A constructor that is assigned the given name
    /// </summary>
    /// <param name="name">name for the state</param>
    /// <returns>A newly constructed state with name=name</returns>
    public State(string name) {
        this.name = name;
    }

    /// <summary>
    /// Behavior of this state run every frame
    /// </summary>
    public abstract void UpdateBehavior();
    /// <summary>
    /// Behavior of this state run when this state is left
    /// </summary>
    /// <param name="newState">The state replacing this state</param>
    public abstract void OnExit(State newState);
    /// <summary>
    /// Behavior of this state run when this state is entered
    /// </summary>
    /// <param name="oldState">The state being replaced by this state</param>
    public abstract void OnEnter(State oldState);
}
