using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EnemyState { IDLE, ACTIVE, RETREATING, DEAD }
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
    protected float speed;
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
    protected EnemyController enemyController;
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        weaponUser = GetComponent<EnemyWeaponUser>();
        enemyController = GetComponent<EnemyController>();
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
                    if(transform.position.x < targetX - targetThreshold || transform.position.x > targetX + targetThreshold) {
                        // Move to the new position
                        Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
                        if(targetX < transform.position.x) {
                            transform.Translate(Vector3.left * Time.deltaTime * walkSpeed, Space.World);
                        } else {
                            transform.Translate(Vector3.right * Time.deltaTime * walkSpeed, Space.World);
                        }
                    
                    } else {
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
                // Direct the weapon at the target
                weaponUser.target = view.interest.transform.position;
                // Determine if the target is in range
                if(Vector3.Distance(transform.position, weaponUser.target) > ((RangedWeapon) weaponUser.currentWeapon).range)
                    {
                    // Move to the player
                    if (weaponUser.target.x < transform.position.x)
                    {
                        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
                    }
                    else
                    {
                        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
                    }
                }
                else if(Time.time >= nextFireTime) {
                    weaponUser.Attack();
                    nextFireTime = Time.time + fireDelay;
                    }
                break;
        }
    }

    protected void OnHealthDecrease() {
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
}
