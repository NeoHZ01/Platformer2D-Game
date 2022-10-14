using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rgbody2d;
    public BoxCollider2D boxcollider2d;
    public float MovementSpeed = 20f;
    public float jumpAmount = 2f;
    public bool isControlling = true;

    private Animator playerAnim;
    private Vector3 moveDelta;

    private float jumpDelay = 1f;
    private bool isJump;
    private int timer;

    //private float damageDelay = 1f;
    //private bool isCollidingEnemy;
    private int immuneTime;

    //private Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponent<Animator>(); // Get animator
        Idle();

        //isCollidingEnemy = false;
        isJump = false; // Set private bool (isJump) to false

        if (jumpDelay <= 0) // If jumpdelay is less than or equal to 0
        {
            jumpDelay = 1; // Set it to 1
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal input
        float x = Input.GetAxisRaw("Horizontal");

        // Reset Move Delta
        moveDelta = new Vector3(x, 0, 0);
        
        // Change sprite direction
        if (moveDelta.x > 0 && Time.timeScale == 1)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0 && Time.timeScale == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (isControlling)
        {
            if (Input.GetKey(KeyCode.A)) // If press A
            {
                //myspriteRenderer.flipX = true; // Flip X of sprite if moving left
                Run();
                transform.Translate(moveDelta * Time.deltaTime * MovementSpeed); // Move left
            }
            if (Input.GetKey(KeyCode.D)) // If press D
            {
                //myspriteRenderer.flipX = false; // Do not flip X of sprite if moving right
                Run();
                transform.Translate(moveDelta * Time.deltaTime * MovementSpeed); // Move right
            }

            if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false) 
            {
                Idle();
            }
            if (Input.GetKey(KeyCode.Space) && isJump == false) // If press spacebar and isJump bool is false
            {
                rgbody2d.velocity = Vector2.up * jumpAmount; // Jump up using jumpamount and velocity
                timer += 1; // Timer used to calculate how long the player can stay in the air for (while pressing down the "space" key)
                if (timer > 5) // If timer is more than 5
                {
                    StartCoroutine("BlockJumpSpam"); // Start coroutine
                }
                else // Otherwise
                {
                    isJump = false; // Jump is false 
                }
            }
        }
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fighter"  && collision.gameObject.name == "Enemy" && !isCollidingEnemy)
        {
            Damaged();
            immuneTime += 1;
        }
        if (immuneTime > 5)
        {
            StartCoroutine("BlockDamageSpam");
        }
        else
        {
            isCollidingEnemy = false;
        }
    } */

    //IEnumerator BlockDamageSpam() // Coroutine to prevent player getting spammed with enemy collider
    //{
    //    isCollidingEnemy = true; // Will run if isCollidingEnemy is true
    //    yield return new WaitForSeconds(damageDelay); // Wait for seconds based on damageDelay (1 second)

    //    yield return null;
    //    isCollidingEnemy = false; // Turn isCollidingEnemy back to false
    //    damageDelay = 0; // Reset damagedelay to 0
    //    playerAnim.SetBool("Damaged", false); // Turn off damage animation when coroutine ends
    //}

    IEnumerator BlockJumpSpam() // Coroutine to prevent jump spams
    {
        isJump = true; // Will run if isJump is true
        yield return new WaitForSeconds(jumpDelay); // Wait for seconds based on Jumpdelay (1 second)

        yield return null; // Return null
        isJump = false; // Make isJump bool false
        timer = 0; // Reset timer to 0
    }

    

    private void Run()
    {
        playerAnim.SetBool("Running", true);
        playerAnim.SetBool("Idle", false);
    }

    private void Idle()
    {
        playerAnim.SetBool("Idle", true);
        playerAnim.SetBool("Running", false);
    }
}
