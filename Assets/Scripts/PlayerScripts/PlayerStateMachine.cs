using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

/// <summary>
/// State machine for controlling the player
/// </summary>
public class PlayerStateMachine : StateMachine
{
    public override State state {
        get { return base._state; }
        set {
            base.state = value; // Base behavior
            stateLabel.SetText(value.name); // Updating the label of the state
        }
    }
    private State idleState;
    /// <summary>
    /// Label used to keep track of the state
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI stateLabel;
    /// <summary>
    /// PlayerController used to measure speed, etc.
    /// </summary>
    private PlayerController playerController;
    private bool _dead;
    public bool dead {
        set { 
                _dead = value; 
                if(value) {
                    state = new DeadState();
                } else {
                    state = new IdleState();
                }
            }
        get { return _dead; }    
        
    }
    protected new void Start() {
        base.Start();
        // Setting the default state
        dead = false;
        playerController = GetComponent<PlayerController>();
    }

    protected new void Update() {
        base.Update();
        // if (playerController.playerVelocity.y < 0 && !(state is FallingState)) {
        //     state = new FallingState();
        // } else if (playerController.playerVelocity.y >= 0 && !(state is IdleState)) {
        //     state = new IdleState();
        // }
    }

    
}
