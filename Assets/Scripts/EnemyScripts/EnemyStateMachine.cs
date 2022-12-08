using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EnemyState { IDLE, ACTIVE, RETREATING, COVER, DEAD }
public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState _state;
    [SerializeField]
    protected EnemyState state{
        get { return _state; }
        set {
            _state = value;
            stateText.text = _state.ToString();
        }
    }
    [SerializeField]
    protected float moveDelay;
    [SerializeField]
    protected float runSpeed;
    [SerializeField]
    protected float walkSpeed;
    [SerializeField]
    protected float wanderDistance;
    [SerializeField]
    protected Transform head;
    [SerializeField]
    protected EnemyView view;
    [SerializeField]
    protected float fireDelay;
    [SerializeField]
    protected TextMeshProUGUI stateText;
    protected float targetX;
    protected float targetThreshold = .1f;
    private float nextMoveTime;
    private float nextFireTime;
    protected Rigidbody rigidbody;
    protected EnemyWeaponUser weaponUser;
    protected EnemyController controller;
    protected EnemyCoverUser coverUser;
    protected Cover nearestCover;
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
                    weaponUser.Attack();
                    nextFireTime = Time.time + fireDelay;
                    }
                break;
            case (EnemyState.RETREATING):
                if(nearestCover.occupied) {
                    // Find a new cover
                    nearestCover = coverUser.GetNearestUnoccupiedCover(transform.position);
                    targetX = nearestCover.coverPoint.transform.position.x;
                }
                // Run into the cover
                if(!MoveToTargetX(runSpeed)) {
                    StartCoroutine(WaitInCover(5, nearestCover));
                }
                break;
        }
    }

    protected void OnHealthDecrease() {
        // Find the nearest cover
        nearestCover = coverUser.GetNearestUnoccupiedCover(transform.position);
        targetX = nearestCover.coverPoint.transform.position.x;
        state = EnemyState.RETREATING;
    }
    protected void OnDestroyed() {
        state = EnemyState.DEAD;
    }
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
