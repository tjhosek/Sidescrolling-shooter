using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Defines the attributes and behaviour for a cover
/// </summary>
public class Cover : MonoBehaviour
{
    [SerializeField]
    public GameObject coverPoint; // Point at which the player takes cover
    [SerializeField]
    private TextMeshProUGUI debugText; // debug label to display cover's value
    private bool _occupied;
    public bool occupied {
        get {
            return _occupied;
        }
        set {
            _occupied = value;
        }
    }

    public void SetDebugLabel(string text)
    {
        debugText.text = text;
    }
}
