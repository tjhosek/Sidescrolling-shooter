using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A character capable of using a weapon
/// </summary>
public class WeaponUser : MonoBehaviour
{
    [SerializeField]
    protected Weapon _currentWeapon;
    [SerializeField]
    protected bool canUseRangedWeapons;
    [SerializeField]
    protected bool canUseMeleeWeapons;
    public Weapon currentWeapon {
        get { return _currentWeapon; }
        set {
            if (value is RangedWeapon && canUseRangedWeapons) {
                _currentWeapon = value;
            } else if (canUseMeleeWeapons) { // I have not collapsed this if in case I need to distinguish melee weapons later
                _currentWeapon = value;
            }
        }
    }
    protected Vector3 _target;
    /// <summary>
    /// What this WeaponUser is aiming at
    /// </summary>
    /// <value>the position of the new target to aim at</value>
    public Vector3 target {
        get { return _target; }
        set { _target = value; }
    }

    /// <summary>
    /// Attacks with the weapon at the target
    /// </summary>
    public void Attack() {
        if (currentWeapon is RangedWeapon && canUseRangedWeapons) {
            // Make a ranged attack
            RangedWeapon currentRangedWeapon = (RangedWeapon) currentWeapon;
            // Draw the tracer line for the attack
            LineRenderer tracer = Instantiate(currentRangedWeapon.tracer);
            tracer.positionCount = 2;
            tracer.SetPositions(new Vector3[2] {currentRangedWeapon.transform.position, target});
            // Fire a raycast from the weapon to the target
            RaycastHit hit;
            Ray ray = new Ray(currentRangedWeapon.transform.position, target);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) {
                // Hit a collider
                Collider collider = hit.collider;
                Debug.Log(string.Format("Hit {0} at {1}", collider.ToString(), collider.transform.position.ToString()));
                if (collider.gameObject.TryGetComponent(out IDamageable damageable)) {
                    // If the collider's gameObject can be damaged, damage it the weapon's damage
                    Debug.Log(string.Format("Damaged {0} for {1} damage", collider.ToString(), currentRangedWeapon.damage));
                    damageable.Hurt(currentRangedWeapon.damage);
                }
            }
        } else if (canUseMeleeWeapons) {
            // Make a melee attack
        }
    }
}
