using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : Collidable // Inherit from Collidable
{
    public BoxCollider2D boxcollider2d;

    public GameObject player;
    private float pushbackForce = 1;

    protected override void OnCollide(Collider2D collide)
    {
        if (collide.tag == "Fighter" && collide.name == "Player")
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Vector3 position = player.transform.position;
            Vector2 direction = collide.transform.position - transform.position;
            rb.AddForce(direction.normalized * pushbackForce, ForceMode2D.Impulse);

            GameManager.instance.ReduceHealth();

            //Debug.Log("Pushing");
            //hasEntered = true;
        }
    }
}
