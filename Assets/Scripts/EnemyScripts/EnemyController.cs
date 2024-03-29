using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the health and some basic physics behaviors of the enemy
/// </summary>
public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float _maxHealth;
    [SerializeField]
    protected bool _isDestroyed;
    /// <summary>
    /// The duration of the flinch when shot
    /// </summary>
    [SerializeField]
    protected float baseFlinchDuration; 
    /// <summary>
    /// The distance of the flinch when shot
    /// </summary>
    [SerializeField]
    protected float flinchDistance;
    /// <summary>
    /// The gravity to apply to this character
    /// </summary>
    [SerializeField]
    protected float gravity;
    protected Rigidbody rigidbody;
    
    public float maxHealth { 
        get {return _maxHealth;}
        set {_maxHealth = value;}
        }
    public bool isDestroyed { 
        get {return _isDestroyed;}
        set {
            _isDestroyed = value;
            if(_isDestroyed) {
                SendMessage("OnDestroyed");
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
            if(value < _health) {
                // Character damaged
                SendMessage("OnHealthDecrease");
                StartCoroutine(Flinch(baseFlinchDuration));
            }
            _health = Mathf.Clamp(value, 0, maxHealth); 
            if(_health == 0) {
                isDestroyed = true;
            } else {
                isDestroyed = false;
                }
            }
        }
    private void Start()
    {
        _health = maxHealth;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Applying simple gravity
        float newY = transform.position.y - gravity * Time.deltaTime;
        Vector3 newPosition = new Vector3(transform.position.x, newY, transform.position.z);
        rigidbody.MovePosition(newPosition);
    }

    /// <summary>
    /// Coroutine to give some flinch to the enemy when shot
    /// </summary>
    /// <param name="flinchDuration"></param>
    /// <returns></returns>
    private IEnumerator Flinch(float flinchDuration)
    {
        float start = Time.time;
        float startX = transform.position.x;
        float finalX = transform.position.x + flinchDistance;
        while (Time.time < start + flinchDuration) {
            float x = Mathf.Lerp(startX, finalX, (Time.time - start) / flinchDuration);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(finalX, transform.position.y, transform.position.z);
    }
}
