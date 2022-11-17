using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines how the Player handles weapons
/// </summary>
public class PlayerWeaponUser : WeaponUser
{
    /// <summary>
    /// Camera used to cast rays
    /// </summary>
    [SerializeField]
    protected Camera cam;
    private float camToPlayerDistance;
    
    void Start()
    {
        // Storing the distance from the camera to the player in order to ensure the gun aims correctly
        camToPlayerDistance = Vector3.Distance(cam.transform.position, transform.position);
    }

    
    void Update()
    {
        // Setting the target to the position of the mouse in the world space
        Vector2 mousePos = Input.mousePosition;
        Vector3 mousePosWithDepth = new Vector3(mousePos.x, mousePos.y, camToPlayerDistance);
        target = cam.ScreenToWorldPoint(mousePosWithDepth);

        // Rotating the gun to point at the target
        currentWeapon.transform.LookAt(target);

        // Fire the weapon if the mouse button is clicked
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                target = new Vector3(hit.point.x, hit.point.y, hit.point.z+.5f);
            }
            Attack();
        }

    }
}
