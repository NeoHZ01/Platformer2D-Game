using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private CircleCollider2D circleCollider2d;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider2d = gameObject.GetComponent<CircleCollider2D>();
    }

    // When collided with game object named as Player, invoke the collected method
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            Collected();
        }
    }

    // Destroy game object when collided and invoke the method from game manager
    public void Collected()
    {
        Destroy(gameObject);
        GameManager.instance.AddCoinCount();
    }

}
