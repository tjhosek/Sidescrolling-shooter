using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// A level end zone.
/// </summary>
public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI levelEndLabel;
    
    protected void Start()
    {
        levelEndLabel.enabled = false;
    }

    protected void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            levelEndLabel.enabled = true;
        }
    }

}
