using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IDamageable
{
    public float maxHealth { get; set; }
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
    public bool isDestroyed { get; protected set; }

    public void Hurt(float damage) {
        health -= damage;
    }

    public void Heal(float healing) {
        health += healing;
    }
}
