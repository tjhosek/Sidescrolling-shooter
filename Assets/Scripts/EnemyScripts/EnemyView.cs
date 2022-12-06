using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Collider _interest;
    public Collider interest { get {return _interest;} }
    private Vector3 _lastKnownPoint;
    public Vector3 lastKnownPoint { get {return _lastKnownPoint;} }
    private void onTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Player Detected!");
            _interest = other;
        }
    }

    private void onTriggerStay(Collider other) {
        if (other.tag == "Player") {
            _lastKnownPoint = other.transform.position;
        }
    }

    private void onTriggerExit(Collider other) {
        if (other.tag == "Player") {
            _lastKnownPoint = other.transform.position;
            _interest = null;
        }
    }
}
