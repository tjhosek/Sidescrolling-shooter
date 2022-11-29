using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State that represents when the player is in cover
/// </summary>
public class CoverState : State {
    public CoverState() {
        this.name = "CoverState";
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
