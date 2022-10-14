using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter; // To filter what the object should collide with

    private BoxCollider2D boxCollider2d;
    private Collider2D[] hitqueue = new Collider2D[5]; // Array to store the hits

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        boxCollider2d.OverlapCollider(filter, hitqueue); // Store the objects collided in the hitqueue

        for (int i = 0; i < hitqueue.Length; i++) // Loop through hit queue array
        {
            if (hitqueue[i] == null) // If hit queue is empty
                continue; // Skip the loop

            OnCollide(hitqueue[i]); // Otherwise, if hit queue is not empty, OnCollide function will be executed

            hitqueue[i] = null; // Reset the array when the loop ends
        }
    }

    protected virtual void OnCollide(Collider2D collide)
    {

    }
}
