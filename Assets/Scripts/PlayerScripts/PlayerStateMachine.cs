using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public enum PlayerState { IDLE, MOVING, CLIMBING, FALLING, SHOOTING, COVER, PEEKING, DEAD }
/// <summary>
/// State machine for controlling the player
/// </summary>
public class PlayerStateMachine : MonoBehaviour
{
    protected PlayerState _state;
    public  PlayerState state {
        get { return _state; }
        set {
            _state = value; // Base behavior
            stateLabel.SetText(value.ToString()); // Updating the label of the state
        }
    }
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
    protected void Start() {
        state = PlayerState.IDLE;
        playerController = GetComponent<PlayerController>();
        playerCoverUser = GetComponent<PlayerCoverUser>();
        playerWeaponUser = GetComponent<PlayerWeaponUser>();
    }

    protected void Update() {
        StateBehavior();
    }

    protected void StateBehavior() {
        switch (state) {
            case PlayerState.IDLE:
                if(playerController.isOnLadder) {
                    state =  PlayerState.CLIMBING;
                }
                else if(playerController.isMoving) {
                    state =  PlayerState.MOVING;
                }
                // if (playerController.playerVelocity.y < 0) {
                //     state = PlayerState.FALLING;
                // }
                else if (playerCoverUser.inCover) {
                    state = PlayerState.COVER;
                }
                else if(playerCoverUser.isPeeking) {
                    state = PlayerState.PEEKING;
                }
                break;
            case PlayerState.CLIMBING:
                if (!playerController.isOnLadder) {
                    state = PlayerState.IDLE;
                }
                break;
            case PlayerState.MOVING:
                if (!playerController.isMoving) {
                    state = PlayerState.IDLE;
                }
                if (playerCoverUser.inCover) {
                    state = PlayerState.COVER;
                }
                break;
            case PlayerState.FALLING:
                if (playerController.playerVelocity.y >= 0) {
                    state = PlayerState.IDLE;
                }
                break;
            case PlayerState.COVER:
                if (!playerCoverUser.inCover) {
                    state = PlayerState.IDLE;
                }
                break;
            case PlayerState.PEEKING:
                if (!playerCoverUser.isPeeking) {
                    state = PlayerState.PEEKING;
                }
                break;
        }
    }
}
