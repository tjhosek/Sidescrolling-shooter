using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines the behavior of the player interacting with cover. Player interaction with cover is determined by input
/// </summary>
public class PlayerCoverUser : CoverUser
{
    [SerializeField]
    private static KeyCode peekKey = KeyCode.LeftAlt;
    [SerializeField]
    private static KeyCode coverKey = KeyCode.LeftShift;
    protected Cover selectedCover;
    [SerializeField]
    private CoverDetector coverDetector;
    
    protected new void Start()
    {
        base.Start();
        selectedCover = null;
        //coverDetector = GetComponent<CoverDetector>();
    }

    protected new void Update()
    {
        base.Update();
        // Determine the selected cover

        // Determining player input
        // Determine if the player is peeking
        if (Input.GetKeyDown(peekKey) && inCover && !isPeeking) {
            StartPeeking();
        }
        else if (Input.GetKeyUp(peekKey) && inCover && isPeeking) {
            StopPeeking();
        }
        // Determine if the player is entering or exiting cover
        if (Input.GetKeyDown(coverKey)) {
            Vector2 mousePos = Input.mousePosition;
            selectedCover = coverDetector.ClosestCover(new Vector3(mousePos.x, mousePos.y, 0));
            if (!inCover && selectedCover != null) {
                EnterCover(selectedCover);
                selectedCover = null;
            }
            else if (inCover) {
                LeaveCover();
            }
        }
    }
}
