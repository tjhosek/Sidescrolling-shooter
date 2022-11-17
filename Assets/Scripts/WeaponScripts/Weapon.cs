using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A weapon, used by a weapon user, as the primary form of dealing damage for most enemies.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _useTime;
    [SerializeField]
    protected bool _automatic;
    public float damage { get { return _damage; } }
    public float useTime { get { return _useTime; } }
    public bool automatic { get { return _automatic; } }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
