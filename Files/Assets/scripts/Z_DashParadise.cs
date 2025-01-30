using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_DashParadise : MonoBehaviour
{
    public Vector3 Yon;
    public Rigidbody2D rb;
    public MovementPlayer player;

    void Start()
    {
    }


    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "character")
        {
            rb.AddForce(Yon * player.speed * 2.2f, ForceMode2D.Impulse);
            player.sayac++;
            this.GetComponent<SpriteRenderer>().enabled = true; 
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
