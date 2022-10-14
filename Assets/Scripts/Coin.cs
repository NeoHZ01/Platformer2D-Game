using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CircleCollider2D circleCollider2d;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            Collected();
        }
    }

    // Destroy game object when hit
    public void Collected()
    {
        Destroy(gameObject);
        GameManager.instance.AddCoinCount();
    }

}
