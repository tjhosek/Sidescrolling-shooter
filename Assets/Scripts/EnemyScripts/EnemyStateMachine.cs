using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Enum for enemy states
/// </summary>
public enum EnemyState { IDLE, ACTIVE, RETREATING, COVER, DEAD }
/// <summary>
/// Determines the AI behavior of the enemy
/// </summary>
public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState _state;
    [SerializeField]
    /// <summary>
    /// The state this enemy is in currently
    /// </summary>
    /// <value></value>
    public EnemyState state{
        get { return _state; }
        set {
            _state = value;
            stateText.text = _state.ToString();
        }
    }
    [SerializeField]
    protected float moveDelay; // Delay between random idle movements
    [SerializeField]
    protected float runSpeed; // Speed when running
    [SerializeField]
    protected float walkSpeed; // Speed when walking
    [SerializeField]
    protected float wanderDistance; // The highest distance an enemy will wander away
    [SerializeField]
    protected Transform head; // The enemy's head. Used for pointing the view at things if applicable
    [SerializeField]
    protected EnemyView view; // The enemy's view
    [SerializeField]
    protected float fireDelay; // The delay between fired shots
    [SerializeField]
    protected TextMeshProUGUI stateText; // Debug text to display the state
    protected float targetX; // Used when determining enemy movement
    protected float targetThreshold = .1f; // Used to estimate position
    private float nextMoveTime; // The next time the enemy will move when idle
    private float nextFireTime; // The next time the enemy will fire when active
    // Different components of the enemy
    protected Rigidbody rigidbody;
    protected EnemyWeaponUser weaponUser;
    protected EnemyController controller;
    protected EnemyCoverUser coverUser;
    protected Cover nearestCover; // Used when retreating to cover
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        weaponUser = GetComponent<EnemyWeaponUser>();
        controller = GetComponent<EnemyController>();
        coverUser = GetComponent<EnemyCoverUser>();
        targetX = transform.position.x;
        nextMoveTime = Time.time + moveDelay;
    }
    protected void Update()
    {
        // Defining state behaviors
        switch (state) {
            case (EnemyState.IDLE):
                // Determining randomized movement
                if (Time.time >= nextMoveTime) {
                    // If the enemy should stop waiting
                    if(!MoveToTargetX(walkSpeed)) {
                        // If already in the new position, set a new position and wait more
                        targetX = transform.position.x + Random.Range(-wanderDistance, wanderDistance);
                        // Turn to the target
                        if(targetX < transform.position.x) {
                            head.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
                        } else {
                            head.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                        }
                        nextMoveTime = Time.time + moveDelay;
                        Debug.Log("Moving to " + targetX);
                    } 
                }
                break;
            case (EnemyState.ACTIVE):
                // if we don't see anything start idleing again
                if (view.interest == null) {
                    state = EnemyState.IDLE;
                }
                // Direct the weapon at the target
                weaponUser.target = view.interest.transform.position;
                // Determine if the target is in range
                if(Vector3.Distance(transform.position, weaponUser.target) > ((RangedWeapon) weaponUser.currentWeapon).range)
                    {
                    // Move to the player
                    if (weaponUser.target.x < transform.position.x)
                    {
                        transform.Translate(Vector3.left * Time.deltaTime * runSpeed, Space.World);
                    }
                    else
                    {
                        transform.Translate(Vector3.right * Time.deltaTime * runSpeed, Space.World);
                    }
                }
                else if(Time.time >= nextFireTime) {
                    // Fire the weapon if the delay has passed, then set a new delay
                    weaponUser.Attack();
                    nextFireTime = Time.time + fireDelay;
                    }
                break;
            case (EnemyState.RETREATING):
                if(nearestCover.occupied) {
                    // Find a new cover
                    nearestCover = coverUser.GetNearestUnoccupiedCover(transform.position);
                    if (nearestCover != null) {
                        targetX = nearestCover.coverPoint.transform.position.x;
                    } else {
                        // If there isn't a closer cover, shoot instead or return to idle if the view doesn't have an interest yet
                        state = view.interest == null ? EnemyState.IDLE : EnemyState.ACTIVE;
                    }
                }
                // Run into cover
                if(!MoveToTargetX(runSpeed)) {
                    StartCoroutine(WaitInCover(5, nearestCover));
                }
                break;
        }
    }
    /// <summary>
    /// Listener for health decrease to know when to retreat
    /// </summary>
    protected void OnHealthDecrease() {
        // Find the nearest cover
        nearestCover = coverUser.GetNearestUnoccupiedCover(transform.position);
        if (nearestCover != null) {
            targetX = nearestCover.coverPoint.transform.position.x;
            state = EnemyState.RETREATING;
        }
    }
    /// <summary>
    /// Listener for when this enemy is destroyed
    /// </summary>
    protected void OnDestroyed() {
        state = EnemyState.DEAD;
    }
    /// <summary>
    /// Listener for when this enemy sees a target
    /// </summary>
    protected void OnInterestDetected() {
        if (state == EnemyState.IDLE) {
            state = EnemyState.ACTIVE;
        }
    }
    /// <summary>
    /// Moves the enemy to the current targetX. Returns true if a move is done, false otherwise
    /// </summary>
    /// <returns>True if the enemy moved, false otherwise</returns>
    protected bool MoveToTargetX(float speed) {
        if(transform.position.x < targetX - targetThreshold || transform.position.x > targetX + targetThreshold) {
            // Move to the new position
            Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            if(targetX < transform.position.x) {
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            } else {
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
            }
            return true;
        }
        return false;
    }
    /// <summary>
    /// Coroutine that puts an enemy in cover, waits, and then leaves cover
    /// </summary>
    /// <param name="time">Time to wait before leaving cover</param>
    /// <param name="cover">Cover to enter</param>
    /// <returns></returns>
    private IEnumerator WaitInCover(float time, Cover cover) {
        coverUser.EnterCover(cover);
        state = EnemyState.COVER;
        yield return new WaitForSeconds(time);
        if(coverUser.inCover) {
            coverUser.LeaveCover();
        }
        state = EnemyState.ACTIVE;
    }
}
