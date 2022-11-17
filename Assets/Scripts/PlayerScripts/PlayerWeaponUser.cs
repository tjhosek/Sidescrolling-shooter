using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponUser : WeaponUser
{
    [SerializeField]
    protected Camera cam;
    private float camToPlayerDistance;
    // Start is called before the first frame update
    void Start()
    {
        camToPlayerDistance = Vector3.Distance(cam.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Setting the target to the position of the mouse in the world space
        Vector2 mousePos = Input.mousePosition;
        Vector3 mousePosWithDepth = new Vector3(mousePos.x, mousePos.y, camToPlayerDistance);
        target = cam.ScreenToWorldPoint(mousePosWithDepth);

        // Rotating the gun to point at the target
        currentWeapon.transform.LookAt(target);

        if(Input.GetMouseButtonDown(0)) {
            Attack();
        }

    }
}
