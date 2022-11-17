using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerScript : MonoBehaviour
{
    [SerializeField]
    protected float stayTime;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(Fade());
    }

    /// <summary>
    /// Fades the object out, then destroys it. Adapted from https://docs.unity3d.com/Manual/Coroutines.html
    /// </summary>
    /// <returns></returns>
    private IEnumerator Fade() {
        Color c = lineRenderer.material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            lineRenderer.material.color = c;
            yield return new WaitForSeconds(.1f);
        }
        //Debug.Log("Coroutine finished");
        Destroy(this);
    }
}
