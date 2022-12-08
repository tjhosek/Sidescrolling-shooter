using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Trigger to detect elligible cover for the player to enter
/// </summary>
public class CoverDetector : MonoBehaviour
{
    private List<Cover> coverInRange;
    protected void Start() {
        coverInRange = new List<Cover>();
    }
    protected void OnTriggerEnter(Collider other) {
        Debug.Log("CoverDetector entered by " + other.gameObject.name);
        if(other.CompareTag("Cover")) {
            Cover cover = other.GetComponent<Cover>();
            coverInRange.Add(cover);
            cover.SetDebugLabel("In Range");
        }
    }
    protected void OnTriggerExit(Collider other) {
        Debug.Log("CoverDetector exited by " + other.gameObject.name);
        if(other.CompareTag("Cover")) {
            Cover cover = other.GetComponent<Cover>();
            coverInRange.Remove(cover);
            cover.SetDebugLabel("Invalid");
        }
    }
    /// <summary>
    /// Returns the closest unoccupied cover
    /// </summary>
    /// <param name="position">position to measure closeness from</param>
    /// <returns>The closest Cover or null if there is no unocupied cover</returns>
    public Cover ClosestUnoccupiedCover(Vector3 position) {
        float dist = Mathf.Infinity;
        Cover closestCover = null;
        for(int i = 0; i < coverInRange.Count; i++) {
            Cover cover = coverInRange[i];
            float newDist = Vector3.Distance(position, cover.transform.position);
            if (newDist < dist && !cover.occupied) {
                closestCover = cover;
                dist = newDist;
            }
        }
        return closestCover;
    }
}
