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
public class PlayerController : MonoBehaviour, ILadderClimber, IDamageable
{
    [SerializeField]
    protected float moveSpeed; // Speed of the player character moving left and right
    [SerializeField]
    protected float jumpHeight; // Speed of the player character jumping
    [SerializeField]
    protected float gravity = -9.8f; // Speed of gravity
    [SerializeField]
    protected TextMeshProUGUI healthLabel; // Label to track health
    [SerializeField]
    protected TextMeshProUGUI gameOverLabel; // Label to display game over when dead
    [SerializeField]
    protected float _climbSpeed; // speed the player climbs ladders
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private bool _isDestroyed;
    private Vector3 _playerVelocity; // Maintains the current velocity of the player
    public Vector3 playerVelocity{ get { return _playerVelocity; } }
    private CharacterController characterController; // character controller component
    private PlayerCoverUser playerCoverUser;
    private bool grounded; // If this character is grounded
    private bool _isOnLadder;
    private bool _isMoving;

    /// <summary>
    /// If the player is moving or not
    /// </summary>
    /// <value></value>
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
    
    public float maxHealth { get {return _maxHealth; } set {_maxHealth = value;} }
    
    public bool isDestroyed { 
        get {return _isDestroyed; } 
        set {
            _isDestroyed = value;
            gameOverLabel.enabled = _isDestroyed;
        } 
    }
    protected float _health;
    public float health {
        get { return _health; }
        set { 
            _health = Mathf.Clamp(value, 0, maxHealth); 
            healthLabel.SetText(String.Format("Health: {0}/{1}",_health,_maxHealth));
            if(_health == 0) {
                isDestroyed = true;
            } else {
                isDestroyed = false;
                }
            }
        }

    private void Start()
    {
        health = maxHealth;
        characterController = GetComponent<CharacterController>();
        playerCoverUser = GetComponent<PlayerCoverUser>();
        grounded = false;
    }

    private void Update()
    {
        if (!playerCoverUser.inCover) {
            // Clamping Z as a bandaid fix for a glitch in the Cover system
            if(transform.position.z != playerCoverUser.foregroundZ) {
                transform.position = new Vector3(transform.position.x, transform.position.y, playerCoverUser.foregroundZ);
                _isMoving = false;
            } else {
                // Storing if the player is grounded at the start of this update
                grounded = characterController.isGrounded;
                // Applying gravity
                _playerVelocity.y += gravity * Time.deltaTime;
                // Ensuring vertical velocity is not decreasing when on the ground
                if(grounded && playerVelocity.y < 0 && !isOnLadder)
                {
                    _playerVelocity.y = 0f;
                }
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
        } else {
            _isMoving = false;
        }
    }
}
