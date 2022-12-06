using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Collider _interest;
    public Collider interest { get {return _interest;} }
    private Vector3 _lastKnownPoint;
    public Vector3 lastKnownPoint { get {return _lastKnownPoint;} }
    protected void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player Detected!");
            _interest = other;
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
