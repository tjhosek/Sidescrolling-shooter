using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// Defines a character that can use ladders
/// </summary>
public interface ILadderClimber
{
    /// <summary>
    /// Defines if the character is on a ladder
    /// </summary>
    /// <value></value>
    public bool isOnLadder { get; set; }
    /// <summary>
    /// The speed at which this character can climb a ladder
    /// </summary>
    /// <value></value>
    public float climbSpeed { get; set; }
}
