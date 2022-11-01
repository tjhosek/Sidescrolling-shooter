using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State that represents when the player is not moving
/// </summary>
public class IdleState : State {
    public IdleState() {
        this.name = "IdleState";
    }
    public override void UpdateBehavior() {
        
    }
    
    public override void OnEnter(State oldState) {
        if (oldState != null) {
            Debug.Log(string.Format("Entered {0} from {1}", name, nameof(oldState)));
        }
    }
    public override void OnExit(State newState) {
        if (newState != null) {
            Debug.Log(string.Format("left {0} to {1}", name, nameof(newState)));
        }
    }
}
