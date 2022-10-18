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

    // Update is called once per frame
    protected override void Update()
    {
        base.Start(); // To invoke the update function in the Collidable script - ensure the weapon has collision function
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
