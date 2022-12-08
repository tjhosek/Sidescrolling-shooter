using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Defines the behavior for enemies using a weapon
/// </summary>
public class EnemyWeaponUser : WeaponUser
{
    

    // Start is called before the first frame update
    protected new void Start() {

    }

    // Update is called once per frame
    protected new void Update() {
        base.Update();
        if(target != null) {
            currentWeapon.transform.LookAt(target);
        }
    }
}
