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
public class PlayerController : MonoBehaviour
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

    private Vector3 _playerVelocity; // Maintains the current velocity of the player
    public Vector3 playerVelocity{ get { return _playerVelocity; } }

    private CharacterController characterController;
    private PlayerCoverUser playerCoverUser;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCoverUser = GetComponent<PlayerCoverUser>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!playerCoverUser.inCover) {
            // Storing if the player is grounded at the start of this update
            bool grounded = characterController.isGrounded;
            //groundedDebugLabel.SetText("grounded: " + grounded);
            // Ensuring vertical velocity is not decreasing when on the ground
            //velocityDebugLabel.SetText("Velocity.y: " + _playerVelocity.y);
            // Getting horizontal movement
            float x = Input.GetAxis("Horizontal") * moveSpeed;
            // Applying horizontal movement
            Vector3 move = new Vector3(x, 0, 0);

            // Checking for jumps
            if (Input.GetButtonDown("Jump") && grounded)
            {
                
                move.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
                Debug.Log("Jump! " + move.y);
            }

            //characterController.Move(move * Time.deltaTime);
            characterController.SimpleMove(move);

            
            // Applying gravity
            

            // Apply vertical movement
            //characterController.Move(_playerVelocity * Time.deltaTime);

        }
    }

    private void CheckCoverInput()
    {

    }
}
