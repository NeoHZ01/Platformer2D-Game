using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public ContactFilter2D filter; // To filter what the object should collide with

    private Collider2D[] hitqueue = new Collider2D[5]; // An array to store the collided objects

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        boxCollider2D.OverlapCollider(filter, hitqueue); // Store the objects collided (fliter) in the array (hitqueue)

        for (int i = 0; i < hitqueue.Length; i++) // Loop through the hitqueue array
        {
            if (hitqueue[i] == null) // If hit queue is empty
                continue; // Skip the loop

            if (hitqueue[i].tag == "Weapon" && hitqueue[i].name == "Weapon") // If the element in the hitqueue array is tagged as Weapon and named as Weapon
            {
                Death(); // Invoke death function
            }
        }
    }

    // Destroy game object when hit
    public void Death()
    {
        Destroy(gameObject);
        GameManager.instance.AddKillCount();
    }
    
}
