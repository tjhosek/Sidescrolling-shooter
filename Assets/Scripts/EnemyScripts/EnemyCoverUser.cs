using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines how enemies interact with cover
/// </summary>
public class EnemyCoverUser : CoverUser
{
    protected List<Cover> coverList; // List of all covers

    protected new void Start() {
        base.Start();
        // Get all the covers and keep track of them
        GameObject[] coverObjects = GameObject.FindGameObjectsWithTag("Cover");
        coverList = new List<Cover>(coverObjects.Length);
        foreach( GameObject coverObject in coverObjects ) {
            coverList.Add(coverObject.GetComponent<Cover>());
        }
    }

    /// <summary>
    /// Returns the nearest unoccupied cover from a position, or "null" if there are no unoccupied covers
    /// </summary>
    /// <param name="position"></param>
    /// <returns>The nearest cover or null</returns>
    public Cover GetNearestUnoccupiedCover(Vector3 position) {
        Cover bestCover = null;
        float dist = Mathf.Infinity;
        foreach(Cover cover in coverList) {
            float newDist = Vector3.Distance(cover.coverPoint.transform.position, position);
            if(newDist < dist && !cover.occupied) {
                bestCover = cover;
                dist = newDist;
            }
        }
        return bestCover;
    }

}
