using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the behavior of a character that can take cover.
/// </summary>
public class CoverUser : MonoBehaviour
{
    private Cover currentCover; // The current cover the character is in
    private Boolean _inCover; // If the character is in cover
    /// <summary>
    /// True if this character is in cover, false otherwise
    /// </summary>
    public Boolean inCover { get { return _inCover; }  }
    private Boolean _isPeeking; // If the character is peeking from cover
    public Boolean isPeeking { get { return _isPeeking; } }

    [SerializeField]
    private static float foregroundZ = 0; // The z value for the foreground, used when exiting from cover to return the character to the foreground

    private void Start()
    {
        _inCover = false;
        _isPeeking = false;
    }

    /// <summary>
    /// Moves the CoverUser to behind the cover and sets that Cover as the currentCover
    /// </summary>
    /// <param name="newCover">The cover to move to</param>
    public void EnterCover(Cover newCover)
    {
        // Move to the new cover
        Vector3 newPosition = newCover.coverPoint.transform.position;
        newPosition.y = 0;
        // TODO: tween this
        transform.position = newPosition;

        // Set the currentCover
        currentCover = newCover;

        // Set inCover
        _inCover = true;

        // Setting Debug Label
        currentCover.SetDebugLabel("Occupied");
    }

    /// <summary>
    /// Leaves the current cover provided there is one
    /// </summary>
    public void LeaveCover()
    {
        if (_inCover)
        {
            // Leave the currentCover
            currentCover.SetDebugLabel("Unoccupied");
            currentCover = null;

            // Set the new position
            Vector3 newPosition = transform.position;
            newPosition.z = foregroundZ;

            // Set inCover
            _inCover = false;
        }
    }

    /// <summary>
    /// Starts peeking out of cover
    /// </summary>
    public void StartPeeking()
    {
        _isPeeking = true;
        currentCover.SetDebugLabel("Peeking");
    }

    /// <summary>
    /// Stops peeking out of cover
    /// </summary>
    public void StopPeeking()
    {
        _isPeeking = false;
        currentCover.SetDebugLabel("Occupied");
    }
}
