using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player movement
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    protected float moveSpeed; // Speed of the player character moving left and right
    [SerializeField]
    protected float jumpSpeed; // Speed of the player character jumping

    // Update is called once per frame
    private void Update()
    {
        // Getting horizontal movement
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float y = 0f;
        if(Input.GetButtonDown("Jump"))
        {
            y += Time.deltaTime * jumpSpeed;
        }
        // Apply movement
        transform.position += transform.TransformDirection(new Vector3(x, y, 0));

    }
}
