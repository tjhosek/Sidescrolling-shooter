using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collider entered ladder");
        if (other.gameObject.TryGetComponent(out ILadderClimber ladderClimber)) {
            Debug.Log("ILadderClimber entered ladder");
            ladderClimber.isOnLadder = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Collider exited ladder");
        if (other.gameObject.TryGetComponent(out ILadderClimber ladderClimber)) {
            Debug.Log("ILadderClimber exited ladder");
            ladderClimber.isOnLadder = false;
        }
    }
}
