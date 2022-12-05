using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { IDLE, ACTIVE, RETREATING}
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField]
    protected EnemyState state;
    [SerializeField]
    protected float waitTime;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float walkSpeed;
    [SerializeField]
    protected float wanderDistance;
    // Start is called before the first frame update
    protected float targetX;
    protected float targetThreshold = .1f;
    protected float nextTime;
    protected Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        targetX = transform.position.x;
        nextTime = Time.time + waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case (EnemyState.IDLE):
            if (Time.time >= nextTime) {
                // If the enemy should stop waiting
                if(transform.position.x < targetX - targetThreshold || transform.position.x > targetX + targetThreshold) {
                    // Move to the new position
                    Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
                    transform.Translate(Vector3.right * Time.deltaTime * walkSpeed);
                } else {
                    // If already in the new position, set a new position and wait more
                    targetX = transform.position.x + Random.Range(-wanderDistance, wanderDistance);
                    if(targetX < transform.position.x) {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
                    } else {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                    }
                    nextTime = Time.time + waitTime;
                    Debug.Log("Moving to " + targetX);
                } 
            }
            break;
        }
    }
}
