using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverDetector : MonoBehaviour
{
    private List<Cover> coverInRange;
    protected void Start() {
        coverInRange = new List<Cover>();
    }
    protected void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Cover")) {
            Cover cover = other.GetComponent<Cover>();
            coverInRange.Add(cover);
            cover.SetDebugLabel("In Range");
        }
    }
    protected void OnTriggerExit(Collider other) {
        if(other.CompareTag("Cover")) {
            Cover cover = other.GetComponent<Cover>();
            coverInRange.Remove(cover);
            cover.SetDebugLabel("Invalid");
        }
    }
    public Cover ClosestCover(Vector3 position) {
        float dist = Mathf.Infinity;
        Cover closestCover = null;
        for(int i = 0; i < coverInRange.Capacity; i++) {
            Cover cover = coverInRange[i];
            float newDist = Vector3.Distance(position, cover.transform.position);
            if (newDist < dist) {
                closestCover = cover;
                dist = newDist;
            }
        }
        return closestCover;
    }
}
