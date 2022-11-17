using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerScript : MonoBehaviour
{
    [SerializeField]
    protected float stayTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    /// <summary>
    /// Fades the object out, then destroys it. Adapted from https://docs.unity3d.com/Manual/Coroutines.html
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade() {
        Color c = GetComponent<Renderer>().material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(.1f);
        }
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
