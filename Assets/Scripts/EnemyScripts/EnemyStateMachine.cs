using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EnemyState { IDLE, ACTIVE, RETREATING}
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
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        weaponUser = GetComponent<EnemyWeaponUser>();
        targetX = transform.position.x;
        nextMoveTime = Time.time + moveDelay;
    }
    protected void Update()
    {
        switch (state) {
            case (EnemyState.IDLE):
            // Determining randomized movement
            if (Time.time >= nextMoveTime) {
                // If the enemy should stop waiting
                if(transform.position.x < targetX - targetThreshold || transform.position.x > targetX + targetThreshold) {
                    // Move to the new position
                    Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
                    if(targetX < transform.position.x) {
                        transform.Translate(Vector3.left * Time.deltaTime * walkSpeed);
                    } else {
                        transform.Translate(Vector3.right * Time.deltaTime * walkSpeed);
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
            // If the player is seen then become active
            if (view.interest != null) {
                state = EnemyState.ACTIVE;
            }
            break;

            case (EnemyState.ACTIVE):
            
            
            // Point the view at the player
            head.transform.LookAt(view.lastKnownPoint);

            // Direct the weapon at the target
            weaponUser.target = view.lastKnownPoint;
            if(Time.time >= nextFireTime) {
                weaponUser.Attack();
                nextFireTime = Time.time + fireDelay;
            }
            break;
        }
    }
}
