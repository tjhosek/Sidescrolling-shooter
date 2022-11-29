using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public enum PlayerState { IDLE, MOVING, CLIMBING, FALLING, SHOOTING, COVER, PEEKING, DEAD }
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
    private PlayerCoverUser playerCoverUser;
    private PlayerWeaponUser playerWeaponUser;
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
        playerCoverUser = GetComponent<PlayerCoverUser>();
        playerWeaponUser = GetComponent<PlayerWeaponUser>();
    }

    protected new void Update() {
        base.Update();
        // if (playerController.playerVelocity.y < 0 && !(state is FallingState)) {
        //     state = new FallingState();
        // } else if (playerController.playerVelocity.y >= 0 && !(state is IdleState)) {
        //     state = new IdleState();
        // }
    }

    protected PlayerState StateBehavior(PlayerState state) {
        switch (state) {
            case PlayerState.IDLE:
                if(playerController.isOnLadder) {
                    return PlayerState.CLIMBING;
                }
                if(playerController.isMoving) {
                    return PlayerState.MOVING;
                }
                if (playerController.playerVelocity.y < 0) {
                    return PlayerState.FALLING;
                }
                if (playerCoverUser.inCover) {
                    return PlayerState.COVER;
                }
                if(playerCoverUser.isPeeking) {
                    return PlayerState.PEEKING;
                }
                return state;
            case PlayerState.CLIMBING:
                if (!playerController.isOnLadder) {
                    return PlayerState.IDLE;
                }
                return state;
            case PlayerState.MOVING:
                if (!playerController.isMoving) {
                    return PlayerState.IDLE;
                }
                return state;
            case PlayerState.FALLING:
                if (playerController.playerVelocity.y >= 0) {
                    return PlayerState.IDLE;
                }
                return state;
            case PlayerState.COVER:
                if (!playerCoverUser.inCover) {
                    return PlayerState.IDLE;
                }
                return state;
            case PlayerState.PEEKING:
                if (!playerCoverUser.isPeeking) {
                    return PlayerState.PEEKING;
                }
                return state;
            default: return state;
        }
    }
}
