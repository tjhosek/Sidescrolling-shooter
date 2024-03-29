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
    [SerializeField]
    protected LayerMask rangedLayerMask;
    public Weapon currentWeapon {
        get { return _currentWeapon; }
        set {
            if (value is RangedWeapon && canUseRangedWeapons) {
                _currentWeapon = value;
            } else if (canUseMeleeWeapons) { // I have not collapsed this if in case I need to distinguish more weapon types later
                _currentWeapon = value;
            }
        }
    }
    private Ray _lastRay;
    protected Vector3 _target;
    /// <summary>
    /// What this WeaponUser is aiming at
    /// </summary>
    /// <value>the position of the new target to aim at</value>
    public Vector3 target {
        get { return _target; }
        set { _target = value; }
    }

    protected void Update()
    {
        Debug.DrawRay(_lastRay.origin, _lastRay.direction);
    }

    /// <summary>
    /// Attacks with the weapon at the target
    /// </summary>
    public void Attack() {
        if (currentWeapon is RangedWeapon && canUseRangedWeapons) {
            // Make a ranged attack
            RangedWeapon currentRangedWeapon = (RangedWeapon) currentWeapon;
            // Draw the tracer line for the attack
            //LineRenderer tracer = Instantiate(currentRangedWeapon.tracer);
            //tracer.positionCount = 2;
            //tracer.SetPositions(new Vector3[2] {currentRangedWeapon.transform.position, target});

            // Make a particles for the attack
            currentRangedWeapon.shotParticles.Play();
            // Fire a raycast from the weapon to the target
            RaycastHit hit;
            Vector3 direction = (target - currentRangedWeapon.transform.position).normalized;
            direction.z = 0;
            Ray ray = new Ray(transform.position, direction);
            _lastRay = ray;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, rangedLayerMask, QueryTriggerInteraction.Ignore)) {
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
