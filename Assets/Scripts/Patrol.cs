using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed; // Speed of enemy movement
    public float distance; // Distance where the raycast will stop and the enemy will change direction

    private bool movingRight = true; // A boolean to detect if enemy is moving to right

    public Transform groundDetection; // Detect the ground based on the empty game object tied to each enemy game object

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // Move in the right direction of enemy movement speed and time.deltatime

        // Parameters of raycast Origin, Direction, distance (rays length)
        // The ray will start at the position of groundDetection variable, the ray will shoot downward (which will detect if there is a box collider for the ground/platform)
        // The distance refers to where the raycast will stop at
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false) // If the collider hit by the raycast is false
        {
            if (movingRight == true) // Check if player is moving right, if true
            {
                transform.eulerAngles = new Vector3(0, -180, 0); // Flip the player by -180 in the y axis using transform.eulerAngles
                movingRight = false; // Set movingRight boolean to false
            }
            else // Else if the player is not moving right (boolean is false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0); // Flip the player back by setting normalizing the vector3 through transform.eulerAngles
                movingRight = true; // Set movingRight boolean to true
            }
        }
    }
}
