using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A weapon, used by a weapon user, as the primary form of dealing damage for most characters.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _useTime;
    [SerializeField]
    protected bool _automatic;
    /// <summary>
    /// How much damage this weapon deals in a single attack
    /// </summary>
    /// <value></value>
    public float damage { get { return _damage; } }
    /// <summary>
    /// How long this weapon has to wait between attacks
    /// </summary>
    /// <value></value>
    public float useTime { get { return _useTime; } }
    /// <summary>
    /// If true, this weapon does not need to click for every attack
    /// </summary>
    /// <value></value>
    public bool automatic { get { return _automatic; } }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
