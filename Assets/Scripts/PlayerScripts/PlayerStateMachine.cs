using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// State machine for controlling the player
/// </summary>
public class PlayerStateMachine : StateMachine
{
    /// <summary>
    /// State that represents when the player is not moving
    /// </summary>
    protected class IdleState : State {
        public override void UpdateBehavior() {
            
        }
        public override void OnEnter(State oldState) {
            Debug.Log(string.Format("Entered %s from %s", "IdleState", nameof(oldState)));
        }
        public override void OnExit(State newState) {
            Debug.Log(string.Format("Left %s into %s", "IdleState", nameof(newState)));
        }
    }

    public override State state {
        set {
            base.state = value; // Base behavior
            stateLabel.text = nameof(value); // Updating the label of the state
        }
    }
    private State idleState;
    /// <summary>
    /// Label used to keep track of the state
    /// </summary>
    [SerializeField]
    private Label stateLabel;
    protected new void Start() {
        base.Start();
        // Setting the default state
        idleState = new IdleState();
        state = idleState;
    }

    protected new void Update() {
        base.Update();
        // Define new stuff here...
    }
}
