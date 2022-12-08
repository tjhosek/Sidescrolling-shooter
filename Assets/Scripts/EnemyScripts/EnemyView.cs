using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Determines the behaviors for an enemy view, the trigger that serves as a representation of what this enemy can "see"
/// </summary>
public class EnemyView : MonoBehaviour
{
    private Collider _interest;
    /// <summary>
    /// The current interest of this view, i.e. the player
    /// </summary>
    /// <value></value>
    public Collider interest { get {return _interest;} }
    private Vector3 _lastKnownPoint;
    /// <summary>
    /// The last know point of this view's interest
    /// </summary>
    /// <value></value>
    public Vector3 lastKnownPoint { get {return _lastKnownPoint;} }
    protected void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player Detected!");
            _interest = other;
            SendMessageUpwards("OnInterestDetected");
        }
    }

    protected void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            _lastKnownPoint = other.transform.position;
        }
    }

    protected void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            _lastKnownPoint = other.transform.position;
            //_interest = null;
        }
    }
}
