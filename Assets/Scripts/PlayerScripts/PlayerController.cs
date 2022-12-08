using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls player movement
/// </summary>
/// <remarks>
/// Partially adapted from the Unity Scripting Reference at https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
/// </remarks>
public class PlayerController : MonoBehaviour, ILadderClimber
{
    [SerializeField]
    protected float moveSpeed; // Speed of the player character moving left and right
    [SerializeField]
    protected float jumpHeight; // Speed of the player character jumping
    [SerializeField]
    protected float gravity = -9.8f; // Speed of gravity
    [SerializeField]
    protected TextMeshProUGUI groundedDebugLabel;
    [SerializeField]
    protected TextMeshProUGUI velocityDebugLabel;
    [SerializeField]
    protected float _climbSpeed;

    private Vector3 _playerVelocity; // Maintains the current velocity of the player
    public Vector3 playerVelocity{ get { return _playerVelocity; } }
    
    

    private CharacterController characterController;
    private PlayerCoverUser playerCoverUser;
    private bool grounded;
    private bool _isOnLadder;
    private bool _isMoving;

    public bool isMoving {
        get { return _isMoving; }
    }
    public bool isOnLadder { 
        get {return _isOnLadder;}
        set {_isOnLadder = value;}
        }
    public float climbSpeed { 
        get {return _climbSpeed;}
        set {_climbSpeed = value;}
        }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCoverUser = GetComponent<PlayerCoverUser>();
        grounded = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!playerCoverUser.inCover) {
            // Storing if the player is grounded at the start of this update
            grounded = characterController.isGrounded;
            //groundedDebugLabel.SetText("grounded: " + grounded);
            // Applying gravity
            _playerVelocity.y += gravity * Time.deltaTime;
            // Ensuring vertical velocity is not decreasing when on the ground
            if(grounded && playerVelocity.y < 0 && !isOnLadder)
            {
                _playerVelocity.y = 0f;
            }
            
            velocityDebugLabel.SetText("position.z: " + transform.position.z);
            // Getting horizontal movement
            float x = Input.GetAxis("Horizontal") * moveSpeed;
            _isMoving = x != 0;
            // Applying horizontal movement
            Vector3 move = new Vector3(x, 0, 0);
            if(!isOnLadder) {
                move += playerVelocity;
            } else {
                float y = Input.GetAxis ("Vertical") * climbSpeed;
                move.y = y;
            }
            characterController.Move(move * Time.deltaTime);
        }
    }

    private void CheckCoverInput()
    {

    }
}
