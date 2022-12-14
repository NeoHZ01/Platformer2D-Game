using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlatform : MonoBehaviour
{
    private BoxCollider2D boxcollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxcollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // When collided with player game object, lose message will be active from game manager
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            GameManager.instance.loseMessage.gameObject.SetActive(true);
            GameManager.instance.tryAgainButton.gameObject.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
