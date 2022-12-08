using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// Defines an object that can be damaged and destroyed
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// The maximum health this object can have
    /// </summary>
    /// <value></value>
    public float maxHealth { get; set; }
    /// <summary>
    /// The current health this object has
    /// </summary>
    /// <value></value>
    public float health {
        get { return health; }
        set { 
                health = Mathf.Clamp(value, 0, maxHealth); 
                if(health == 0) {
                    isDestroyed = true;
                } else {
                    isDestroyed = false;
                }
            }
    }
    /// <summary>
    /// Is this object's health 0
    /// </summary>
    /// <value></value>
    public bool isDestroyed { get; set; }
    /// <summary>
    /// Damage this object
    /// </summary>
    /// <param name="damage">the amount of health points to damage this object by</param>
    public void Hurt(float damage) {
        health -= damage;
    }
    /// <summary>
    /// Heal this object
    /// </summary>
    /// <param name="healing">The amount of healing to give to this object</param>
    public void Heal(float healing) {
        health += healing;
    }
}
