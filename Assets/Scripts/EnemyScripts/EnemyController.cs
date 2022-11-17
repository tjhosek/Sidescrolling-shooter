using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float _maxHealth;
    [SerializeField]
    protected bool _isDestroyed;
    public float maxHealth { 
            get {return _maxHealth;}
            set {_maxHealth = value;}
        }
    public bool isDestroyed { 
            get {return _isDestroyed;}
            set {
                _isDestroyed = value;
                if(_isDestroyed) {
                    transform.Rotate(new Vector3(0,0,90));
                    Vector3 newPos = transform.position;
                    newPos.y = .5f;
                    transform.position = newPos;
                    GetComponent<Collider>().enabled = false;
                    }
                }
        }

    protected float _health;
    public float health {
        get { return _health; }
        set { 
                _health = Mathf.Clamp(value, 0, maxHealth); 
                if(_health == 0) {
                    isDestroyed = true;
                } else {
                    isDestroyed = false;
                }
            }
        }
    void Start()
    {
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
