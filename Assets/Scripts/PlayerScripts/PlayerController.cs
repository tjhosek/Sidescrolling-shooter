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

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {

        // Storing if the player is grounded at the start of this update
        bool grounded = characterController.isGrounded;
        groundedDebugLabel.SetText("grounded: " + grounded);
        // Ensuring vertical velocity is not decreasing when on the ground
        if(grounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        } else {
            _playerVelocity.y += gravity * Time.deltaTime;
        }
        velocityDebugLabel.SetText("Velocity.y: " + _playerVelocity.y);
        // Getting horizontal movement
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        // Applying horizontal movement
        Vector3 move = new Vector3(x, 0, 0);
        characterController.Move(move * Time.deltaTime);

        // Checking for jumps
        if(Input.GetButtonDown("Jump") && grounded)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        // Applying gravity
        

        // Apply vertical movement
        characterController.Move(_playerVelocity * Time.deltaTime);
        
    }
}