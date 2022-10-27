using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Used to manage the behavior of the crosshair
/// </summary>
public class Crosshair : MonoBehaviour
{
    [SerializeField]
    protected RectTransform crosshair; // UI element of the crosshair

    protected void Update()
    {
        Vector2 mousePos = Input.mousePosition; // Get the position of the mouse
        crosshair.position = mousePos; // Set the crosshair's position to the mouse's position
        if( mousePos.x < Screen.width && // If the mouse is within the bounds of the game window...
            mousePos.y < Screen.height &&
            mousePos.x >= 0 &&
            mousePos.y >= 0)
        {
            Cursor.visible = false; // Set the cursor to be not visible
            crosshair.gameObject.SetActive(true); // Set the crosshair to be active
        } else // If the mouse is NOT within the bounds of the game window
        {
            Cursor.visible = true; // Set the cursor to be visible
            crosshair.gameObject.SetActive(false); // Set the crosshair to be not active
        }
    }
}
