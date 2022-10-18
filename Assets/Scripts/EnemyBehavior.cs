using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : Collidable // Inherit from Collidable
{
    public BoxCollider2D boxcollider2d;

    public GameObject player;
    private float pushbackForce = 1;

    // Override OnCollide method from collidable script
    protected override void OnCollide(Collider2D collide)
    {
        // Check if collided object is tagged as fighter and named as player
        if (collide.tag == "Fighter" && collide.name == "Player")
        {
            // If true, get rb of player and player vector3 position
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Vector3 position = player.transform.position;

            // Direction of force will be the collided object transform position and the enemy transform position
            Vector2 direction = collide.transform.position - transform.position;

            // Add force to the rigidbody of player game object, normalized the direction multiplied by pushback force using Forcemode2D.impulse
            rb.AddForce(direction.normalized * pushbackForce, ForceMode2D.Impulse);

            // Invoke reduce health method from game manager
            GameManager.instance.ReduceHealth();
        }
    }
}
