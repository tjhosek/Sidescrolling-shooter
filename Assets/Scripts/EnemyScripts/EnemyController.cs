using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float _maxHealth;
    [SerializeField]
    protected bool _isDestroyed;
    [SerializeField]
    protected float baseFlinchDuration;
    [SerializeField]
    protected float flinchDistance;
    
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
            if(value < _health) {
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
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

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
