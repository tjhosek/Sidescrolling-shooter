using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoverUser : CoverUser
{
    protected List<Cover> coverList;

    protected new void Start() {
        base.Start();
        GameObject[] coverObjects = GameObject.FindGameObjectsWithTag("Cover");
        coverList = new List<Cover>(coverObjects.Length);
        foreach( GameObject coverObject in coverObjects ) {
            coverList.Add(coverObject.GetComponent<Cover>());
        }
    }

    public Cover GetNearestUnoccupiedCover(Vector3 position) {
        Cover bestCover = coverList[0];
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
