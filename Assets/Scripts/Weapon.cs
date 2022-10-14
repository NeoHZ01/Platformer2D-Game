using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable // Inherit from Collidable
{
    private Animator swordAnim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // Call for the start from collidable script
        swordAnim = GetComponent<Animator>();
    }

    //protected override void OnCollide(Collider2D collide)
    //{
    //    if (collide.tag == "Fighter")
    //    {
    //        if (collide.name == "Enemy")
    //        {
    //            Debug.Log("Enemy Hit!");
    //        }

    //        return;
            
    //    }
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    
    //    //var force = position - collision.transform.position;
    //    if (collision.gameObject.tag == "Minion")
    //    {
    //        Debug.Log("Hit");
    //        Debug.Log(collision.gameObject);
    //    }
    //}


    // Update is called once per frame
    protected override void Update()
    {
        base.Start(); // To inoke the update function in the Collidable script - To ensure the weapon has collision function
        if (Input.GetMouseButtonDown(0))
        {
            Swing();
        }
    }

    // Function to trigger the swing animation of the weapon
    private void Swing()
    {
        swordAnim.SetTrigger("Swing");

    }

}
