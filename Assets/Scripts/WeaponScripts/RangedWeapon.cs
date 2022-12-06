using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// A weapon that can be used at a distance
/// </summary>
public class RangedWeapon : Weapon
{
    [SerializeField]
    protected int capacity;
    [SerializeField]
    protected float _range;
    public float range
    {
        get { return _range; }
        set { _range = value; }
    }
    [SerializeField]
    protected float reloadTime;
    [SerializeField]
    protected LineRenderer _tracer;
    public LineRenderer tracer { get { return _tracer; } }
    /// <summary>
    /// Defines the point on the weapon to fire from. Currently unimplemented
    /// </summary>
    [SerializeField]
    protected GameObject muzzle;
    protected int _currentAmmo;
    public int currentAmmo {
        get {return _currentAmmo; }
        set {
            _currentAmmo = Mathf.Clamp(value, 0, capacity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = capacity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
