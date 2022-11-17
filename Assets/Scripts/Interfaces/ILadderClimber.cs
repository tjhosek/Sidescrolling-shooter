using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface ILadderClimber
{
    public bool isOnLadder { get; set; }
    public float climbSpeed { get; set; }
}
