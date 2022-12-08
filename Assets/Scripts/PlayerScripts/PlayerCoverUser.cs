using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Determines the behavior of the player interacting with cover. Player interaction with cover is determined by input
/// </summary>
public class PlayerCoverUser : CoverUser
{
    private static KeyCode peekKey = KeyCode.LeftAlt;
    private static KeyCode coverKey = KeyCode.LeftShift;
    protected Cover selectedCover;
    [SerializeField]
    private CoverDetector coverDetector;
    [SerializeField]
    protected TextMeshProUGUI coverDebugLabel;
    
    protected new void Start()
    {
        base.Start();
        selectedCover = null;
        //coverDetector = GetComponent<CoverDetector>();
    }

    protected new void Update()
    {
        base.Update();
        coverDebugLabel.SetText(_inCover.ToString());
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
            if (inCover) {
                Debug.Log("Exiting Cover...");
                LeaveCover();
                Debug.Log("transform.position: " + transform.position.ToString());
            } else {
                // Determine the selected cover by the one closest to the mouse cursor
                Vector2 mousePos = Input.mousePosition;
                selectedCover = coverDetector.ClosestUnoccupiedCover(new Vector3(mousePos.x, mousePos.y, 0));
                if (selectedCover != null) {
                    Debug.Log("Entering Cover...");
                    EnterCover(selectedCover);
                    selectedCover = null;
                }
            }
        }
    }
}
